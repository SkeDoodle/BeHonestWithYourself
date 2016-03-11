using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using System.IO.IsolatedStorage;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Controls.Primitives;
using Coding4Fun.Toolkit.Controls;
using Microsoft.Phone.Shell;

namespace SuperPotatoesPower
{
    public partial class Agenda : PhoneApplicationPage, INotifyPropertyChanged
    {
        #region Properties

        public UserProfile User { get; set; }

        private ObservableCollection<TaskBox> taskBoxes;
        public ObservableCollection<TaskBox> TaskBoxes
        {
            get
            {
                return taskBoxes;
            }
            set
            {
                if (value == taskBoxes)
                    return;
                taskBoxes = value;
                NotifyPropertyChanged("TaskBoxes");
            }
        }

        public TaskBox UnvalidatedTaskBox;
        public bool IsThereUnvalidatedTask
        {
            get
            {
                return (UnvalidatedTaskBox.Tasks.Count != 0 && Day == DateTime.Today);
            }
        }

        public DateTime Day { get; set; }
        public string DayName1
        {
            get
            {
                switch (actualItem)
                {
                    case 0:
                        return this.DateFormat(Day);
                    case 1:
                        return this.DateFormat(Day.AddDays(2));
                    case 2:
                        return this.DateFormat(Day.AddDays(1));
                    default:
                        return "";
                }
            }
        }
        public string DayName2
        {
            get
            {
                switch (actualItem)
                {
                    case 0:
                        return this.DateFormat(Day.AddDays(1));
                    case 1:
                        return this.DateFormat(Day);
                    case 2:
                        return this.DateFormat(Day.AddDays(2));
                    default:
                        return "";
                }
            }
        }
        public string DayName3
        {
            get
            {
                switch (actualItem)
                {
                    case 0:
                        return this.DateFormat(Day.AddDays(2));
                    case 1:
                        return this.DateFormat(Day.AddDays(1));
                    case 2:
                        return this.DateFormat(Day);
                    default:
                        return "";
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private int actualItem = 0;

        #endregion

        // Constructor
        public Agenda()
        {
            InitializeComponent();

            #region Getting User Profile in IsolatedStorage
            if (!IsolatedStorageSettings.ApplicationSettings.Contains(UserProfile.USER))
            {
                User = new UserProfile();
                IsolatedStorageSettings.ApplicationSettings.Add(UserProfile.USER, User);
                IsolatedStorageSettings.ApplicationSettings.Save();
            }
            else
            {
                User = (UserProfile)IsolatedStorageSettings.ApplicationSettings[UserProfile.USER];
            }
            #endregion

            #region Getting day in PhoneApplicationService
            if (!PhoneApplicationService.Current.State.ContainsKey("Day"))
            {
                PhoneApplicationService.Current.State["Day"] = DateTime.Today;
            }

            Day = (DateTime)PhoneApplicationService.Current.State["Day"];
            #endregion

            TaskBoxes = TaskBoxManager.getTaskBoxes(User.TaskList, Day);

            User.Save();
            DataContext = this;
        }

        public string DateFormat(DateTime date)
        {
            return string.Format("{0} {1}", date.ToString("MMMM"), date.Day.ToString());
        }

        #region Events
        
        public void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Modify_Click(object sender, RoutedEventArgs e)
        {
            PhoneApplicationService.Current.State["Task"] = (Task)(sender as MenuItem).DataContext;
            this.NavigationService.Navigate(new Uri("/Task/ModifyTask.xaml", UriKind.Relative));
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            MessagePrompt deleteMessage = new MessagePrompt
            {
                Title = "Delete a task",
                Message = "Do you really want to delete this task ?",
                IsCancelVisible = true
            };
            deleteMessage.Completed += (me, margs) =>
            {
                if (margs.PopUpResult != PopUpResult.Cancelled)
                {
                    Task task = (Task)(sender as MenuItem).DataContext;
                    User.TaskList.Remove(task);
                    TaskBoxes = TaskBoxManager.getTaskBoxes(User.TaskList, Day);
                }
            };
            deleteMessage.Show();
        }

        private void Task_Click(object sender, RoutedEventArgs e)
        {
            Task task = (Task)(sender as CheckBox).DataContext;
            task.IsValidated = (bool)(sender as CheckBox).IsChecked;
            //TaskBoxes = TaskBoxManager.getTaskBoxes(TaskList, Day);
            int points = task.Points;

            int userPreviousGolds = User.Gold;
            int userPreviousLevel = User.Level;

            int exp = (task.EstimatedTime.Hours * 60 + task.EstimatedTime.Minutes)/5;
            int gold = (task.EstimatedTime.Hours * 60 + task.EstimatedTime.Minutes) / 3;
            if (task.IsValidated)
            {
                if (points >= 0)
                {
                    User.refreshUser(points,exp,0,gold);
                }
                else
                {
                    User.refreshUser(0,exp,points,0);
                }
            }
            else
            {
                if (points >= 0)
                {
                    User.refreshUser(-1*points,-1*exp,0,-1*gold);
                }
                else
                {
                    User.refreshUser(0,-1*exp,-1*points,0);
                }
            }

            #region Pop-up level up
            if (userPreviousLevel < User.Level)
            {
                MessagePrompt newLevelUpMessage = new MessagePrompt
                {
                    Title = "Level Up !",
                    Message = "You are now level " + User.Level + ", and can choose a new award in your profile page.",
                };
                newLevelUpMessage.Show();
            }
            #endregion

            #region Pop-up new award available
            bool newAward = false;
            foreach (AwardCategory category in Awards.Categories)
	        {
                foreach(Award award in category.Category)
                {
                    if (award.Golds <= User.Gold && award.Golds > userPreviousGolds)
                    {
                        newAward = true;
                    }
                }
	        }
            if (newAward)
            {
                MessagePrompt newAwardMessage = new MessagePrompt
                {
                    Title = "A new award is available !",
                    Message = "You can now take it in your profile page clicking on the star !",
                };

                newAwardMessage.Show();
            }
            #endregion

        }

        private void To_Calendar(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("/Calendar.xaml", UriKind.Relative));
        }

        private void To_Add(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("/Task/AddTask.xaml", UriKind.Relative));

        }

        private void ProfileBtn_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("/Profile/ProfilePage.xaml", UriKind.Relative));
        }

        private void Pivot_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = Pivot.SelectedIndex;

            if (index == (actualItem + 2) % 3)
            {
                Day = Day.Subtract(TimeSpan.FromDays(1));
                actualItem = index;
                NotifyPropertyChanged("DayName1");
                NotifyPropertyChanged("DayName2");
                NotifyPropertyChanged("DayName3");
            }
            else if (index == (actualItem + 1) % 3)
            {
                Day = Day.AddDays(1);
                actualItem = index;
                NotifyPropertyChanged("DayName1");
                NotifyPropertyChanged("DayName2");
                NotifyPropertyChanged("DayName3");
            }

            TaskBoxes = TaskBoxManager.getTaskBoxes(User.TaskList, Day);
        }
        #endregion
    }
}
