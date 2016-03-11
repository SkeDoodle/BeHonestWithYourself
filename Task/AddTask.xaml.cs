using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.IO.IsolatedStorage;
using Coding4Fun.Toolkit.Controls;

namespace SuperPotatoesPower
{
    public partial class AddTask : PhoneApplicationPage
    {
        private string[] enjoymentSentences = new string[] { "Extremely Negative", "Very Negative", "Negative", "Somewhat Negative", "A bit Negative","Neutral", "Positive", "Barely Positive", "Somewhat Positive", "Mostly Positive", "Very Positive"};


        public AddTask()
        {
            InitializeComponent();

            this.toggle.Checked += new EventHandler<RoutedEventArgs>(toggle_Checked);
            this.toggle.Unchecked += new EventHandler<RoutedEventArgs>(toggle_Unchecked);
        }

        #region Toggle events
        private void toggle_Checked(object sender, RoutedEventArgs e)
        {
            this.task.Text = "Task";
            this.date.Text = "From";
            this.date2.Visibility = System.Windows.Visibility.Visible;
            this.datePicker2.Visibility = System.Windows.Visibility.Visible;
            this.timePicker2.Visibility = System.Windows.Visibility.Visible;
        }

        private void toggle_Unchecked(object sender, RoutedEventArgs e)
        {
            this.task.Text = "Appointment";
            this.date.Text = "Date";
            this.date2.Visibility = System.Windows.Visibility.Collapsed;
            this.datePicker2.Visibility = System.Windows.Visibility.Collapsed;
            this.timePicker2.Visibility = System.Windows.Visibility.Collapsed;
        }
        #endregion

        #region Slider events
        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Slider slider = (Slider)sender;
            slider.Value = Math.Round(e.NewValue);

            int value = (int)slider.Value + 5;
            this.enjoyment_tb.Text = enjoymentSentences[value];
        }
        #endregion

        #region Bottom Menu events
        private void bt_done_Click(object sender, EventArgs e)
        {
            DateTime startDate = ((DateTime)datePicker.Value).AddHours(timePicker.Value.Value.Hour).AddMinutes(timePicker.Value.Value.Minute);
            DateTime deadline = ((DateTime)datePicker2.Value).AddHours(timePicker2.Value.Value.Hour).AddMinutes(timePicker2.Value.Value.Minute);

            if (toggle.IsChecked == true && deadline < startDate)
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
                user.TaskList.Add(new Task
                {
                    Title = taskTitle.Text,
                    StartDate = startDate,
                    Deadline = deadline,
                    EstimatedTime = estimatedTime.Value.Value,
                    Priority = (int)slider.Value,
                    IsRepeated = (occurenceIndex != 0),
                    RepetitionTime = (RepetitionTime)occurenceIndex
                });

                user.Save();
                this.NavigationService.Navigate(new Uri("/Agenda.xaml", UriKind.Relative));
            }
        }

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