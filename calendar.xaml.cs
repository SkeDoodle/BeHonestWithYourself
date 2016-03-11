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
using Microsoft.Phone.Shell;

namespace SuperPotatoesPower
{
    public partial class Calendar : PhoneApplicationPage
    {
        private int index = 0;

        public Calendar()
        {
            index = 0;
            InitializeComponent();
        }

        private void Cal_SelectionChanged(object sender, WPControls.SelectionChangedEventArgs e)
        {
            DateTime day = e.SelectedDate;
            PhoneApplicationService.Current.State["Day"] = day;
            if(index == 1){
                this.NavigationService.Navigate(new Uri("/Agenda.xaml", UriKind.Relative));
            }
            index++;
        }
    }
}