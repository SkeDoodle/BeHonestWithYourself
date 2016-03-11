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
using Microsoft.Phone;
using System.IO.IsolatedStorage;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Microsoft.Phone.Shell;

namespace SuperPotatoesPower
{
    public partial class MainPage : PhoneApplicationPage
    {
        /// <summary>
        /// Constructor of the class MainPage, 
        /// </summary>
        #region Constructor

        public MainPage()
        {
            InitializeComponent();
        }

        #endregion

        /// <summary>
        /// Methods of the page MainPage
        /// </summary>
        /// <param 1name="sender"></param>
        /// <param name="e"></param>
        #region Methods
        
        /// <summary>
        /// If a profile exist, navigate to the page Aganda.xaml
        /// </summary>
        /// <param name="e"></param>
        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            if (IsolatedStorageSettings.ApplicationSettings.Contains(UserProfile.USER) && !PhoneApplicationService.Current.State.ContainsKey("Start"))
            {
                PhoneApplicationService.Current.State.Add("Start", true);
                Start.Visibility = Visibility.Collapsed;
                this.NavigationService.Navigate(new Uri("/Agenda.xaml", UriKind.Relative));
            }
        }

        /// <summary>
        /// Navigate to the page Agenda.xaml
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Start_Click(object sender, RoutedEventArgs e)
        {
            if (!IsolatedStorageSettings.ApplicationSettings.Contains(UserProfile.USER))
            {
                PhoneApplicationService.Current.State.Add("Start", true);
                Start.Visibility = Visibility.Collapsed;
                this.NavigationService.Navigate(new Uri("/Agenda.xaml", UriKind.Relative));
            }
        }
        #endregion
    }
}
