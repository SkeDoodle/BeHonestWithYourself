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
using System.Collections.ObjectModel;
using System.IO.IsolatedStorage;
using System.ComponentModel;
using Coding4Fun.Toolkit.Controls;

namespace SuperPotatoesPower
{
    public partial class AwardList : PhoneApplicationPage, INotifyPropertyChanged
    {
        public const int NEWAWARDPRICE = 500;

        private int gold;
        public int Gold
        {
            get
            {
                return gold;
            }
            set
            {
                if (value == gold)
                    return;
                gold = value;
                NotifyPropertyChanged("Gold");
            }
        }
        public UserProfile User { get; set; }
        private ObservableCollection<AwardCategory> categories;
        public ObservableCollection<AwardCategory> Categories
        {
            get
            {
                return categories;
            }
            set
            {
                if (value == categories)
                    return;
                categories = value;
                NotifyPropertyChanged("Categories");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public AwardList()
        {
            InitializeComponent();
            User = (UserProfile)IsolatedStorageSettings.ApplicationSettings[UserProfile.USER];

            Categories = Awards.Categories;
            this.UpdateView();

            DataContext = this;
        }

        public void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Award_Click(object sender, RoutedEventArgs e)
        {
            string awardText = ((TextBlock)((Button)sender).Content).Text;

            MessagePrompt awardMessage = new MessagePrompt
            {
                Title = "Take an Award ",
                Message = "Do you really want : \n" + awardText + " ?",
                IsCancelVisible = true
            };
            awardMessage.Completed += (me, margs) =>
            {
                if (margs.PopUpResult != PopUpResult.Cancelled)
                {
                    foreach (AwardCategory category in Awards.Categories)
                    {
                        foreach (Award award in category.Category)
                        {
                            if (award.Name == awardText)
                            {
                                User.Gold -= award.Golds;
                            }
                        }
                    }
                    this.UpdateView();

                    MessagePrompt validateAwardMessage = new MessagePrompt
                    {
                        Title = "Take your Award ",
                        Message = "You can now : \n" + awardText + ".",
                    };
                    validateAwardMessage.Show();
                }
            };
            awardMessage.Show();
        }

        private void UpdateView()
        {

            if (User.Gold >= NEWAWARDPRICE)
            {
                AddAward.Foreground = new SolidColorBrush(Colors.White);
            }
            else
            {
                AddAward.Foreground = new SolidColorBrush(Colors.Gray);
            }

            if (User.CustomAwards.Count != 0)
            {
                Categories.Insert(0, new AwardCategory { Name = "My Custom Awards", Category = User.CustomAwards });
            }

            foreach (AwardCategory category in Categories)
            {
                foreach (Award award in category.Category)
                {
                    if (award.Golds <= User.Gold)
                    {
                        award.IsUnlocked = true;
                    }
                    else
                    {
                        award.IsUnlocked = false;
                    }
                }
            }
            
            Gold = User.Gold;
        }

        private void AddAward_Click(object sender, RoutedEventArgs e)
        {
            if (User.Gold >= NEWAWARDPRICE)
            {
                InputPrompt prompt = new InputPrompt();
                InputScope s = new InputScope();
                prompt.InputScope = s;
                prompt.Title = "My Custom Award";
                prompt.Message = "Please enter the text of your award.";

                prompt.Show();
                prompt.Completed += new EventHandler<PopUpEventArgs<string, PopUpResult>>(AddAwardCompleted);

            }
        }

        private void AddAwardCompleted(object sender, PopUpEventArgs<string, PopUpResult> e)
        {
            if (e.Result.Length > 0)
            {
                MessagePrompt addAwardMessage = new MessagePrompt
                {
                    Title = "Add a new award",
                    Message = "Do you really want to add the award : " + e.Result,
                    IsCancelVisible = true
                };

                addAwardMessage.Completed += (me, margs) =>
                {
                    if (margs.PopUpResult != PopUpResult.Cancelled)
                    {
                        User.CustomAwards.Add(new Award { Name = e.Result, Golds = 100 });
                        User.Save();
                        User.Gold -= 500;
                        this.UpdateView();
                    }
                };
                addAwardMessage.Show();
            }
        }
    }
}