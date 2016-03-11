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
using Coding4Fun.Toolkit.Controls;
using Microsoft.Phone.Shell;

namespace SuperPotatoesPower
{
    public partial class ModifyTask : PhoneApplicationPage
    {
        #region Properties

        public Task ModifiedTask { get; set; }

        #endregion

        #region Constructor

        public ModifyTask()
        {
            InitializeComponent();

            this.toggle.Checked += new EventHandler<RoutedEventArgs>(toggle_Checked);
            this.toggle.Unchecked += new EventHandler<RoutedEventArgs>(toggle_Unchecked);
            ModifiedTask = (Task)PhoneApplicationService.Current.State["Task"];
            taskTitle.Text = ModifiedTask.Title;
            DateTime date1 = new DateTime(ModifiedTask.StartDate.Year, ModifiedTask.StartDate.Month, ModifiedTask.StartDate.Day);
            DateTime time1 = new DateTime(ModifiedTask.StartDate.Year, ModifiedTask.StartDate.Month, ModifiedTask.StartDate.Day, ModifiedTask.StartDate.Hour, ModifiedTask.StartDate.Minute, 0);
            DateTime date2 = new DateTime(ModifiedTask.Deadline.Year, ModifiedTask.Deadline.Month, ModifiedTask.Deadline.Day);
            DateTime time2 = new DateTime(ModifiedTask.Deadline.Year, ModifiedTask.Deadline.Month, ModifiedTask.Deadline.Day, ModifiedTask.Deadline.Hour, ModifiedTask.Deadline.Minute, 0);
            datePicker.Value = date1;
            timePicker.Value = time1;
            datePicker2.Value = date2;
            timePicker2.Value = time2;
            occurenceListPicker.SelectedIndex = (int)ModifiedTask.RepetitionTime;
            slider.Value = ModifiedTask.Priority;
            estimatedTime.Value = ModifiedTask.EstimatedTime;
        }

        #endregion

        #region Methods

        /// <summary>
        /// When the toggle is checked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toggle_Checked(object sender, RoutedEventArgs e)
        {

            this.date2.Visibility = System.Windows.Visibility.Visible;
            this.datePicker2.Visibility = System.Windows.Visibility.Visible;
            this.timePicker2.Visibility = System.Windows.Visibility.Visible;
        }

        /// <summary>
        /// When the toggle is unchecked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toggle_Unchecked(object sender, RoutedEventArgs e)
        {
            //this.toggle.Content = "Appointment";
            this.date2.Visibility = System.Windows.Visibility.Collapsed;
            this.datePicker2.Visibility = System.Windows.Visibility.Collapsed;
            this.timePicker2.Visibility = System.Windows.Visibility.Collapsed;
        }

        /// <summary>
        /// When the value of the slider changes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Slider slider = (Slider)sender;
            slider.Value = Math.Round(e.NewValue);
        }

        /// <summary>
        /// Validation of the modification of a task
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_done_Click(object sender, EventArgs e)
        {
            DateTime startDate = ((DateTime)datePicker.Value).AddHours(timePicker.Value.Value.Hour).AddMinutes(timePicker.Value.Value.Minute);
            DateTime deadline = ((DateTime)datePicker2.Value).AddHours(timePicker2.Value.Value.Hour).AddMinutes(timePicker2.Value.Value.Minute);

            if (deadline < startDate)
            {
                MessagePrompt popup = new MessagePrompt
                {
                    Title = "Warning",
                    Message = "Start date must be before end date =)",
                };
                popup.Show();
            }
            else
            {
                int occurenceIndex = occurenceListPicker.SelectedIndex;

                UserProfile user = (UserProfile)IsolatedStorageSettings.ApplicationSettings[UserProfile.USER];
                int index = user.TaskList.IndexOf((Task)PhoneApplicationService.Current.State["Task"]);
                Task task = user.TaskList.ElementAt(index);
                task.Deadline = deadline;
                task.StartDate = startDate;
                task.Title = taskTitle.Text;
                task.RepetitionTime = (RepetitionTime)occurenceListPicker.SelectedIndex;
                task.IsRepeated = (occurenceListPicker.SelectedIndex != 0);
                task.Priority = (int)slider.Value;
                task.EstimatedTime = estimatedTime.Value.Value;
                
                user.Save();
                this.NavigationService.Navigate(new Uri("/Agenda.xaml", UriKind.Relative));
            }
        }

        /// <summary>
        /// Cancellation of the modification of a task
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_cancel_Click(object sender, EventArgs e)
        {
            {
                if (NavigationService.CanGoBack)
                    NavigationService.GoBack();
            }
        }

        #endregion
    }
}