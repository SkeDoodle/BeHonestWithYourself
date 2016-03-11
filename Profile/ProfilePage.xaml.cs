using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Windows.Media.Imaging;
using System.IO.IsolatedStorage;
using Coding4Fun.Toolkit.Controls;

namespace SuperPotatoesPower
{
    public partial class ProfilePage : PhoneApplicationPage
    {
        /// <summary>
        /// Properties of the page ActualProfilePage
        /// </summary>

        #region Properties

        private UserProfile user;
        private const string step1 = "Come on ! Work it out !";
        private const string step2 = "Continue your efforts !";
        private const string step3 = "You're doing good !";
        private const string step4 = "Almost there !";
        private const string step5 = "The end is near !";
        private const string step6 = "You did it !";

        #endregion

        /// <summary>
        /// Constructor of the page ActualProfilePage
        /// </summary>

        #region Constructor

        private const string USER = "_user";

        public ProfilePage()
        {
            InitializeComponent();
            if (!IsolatedStorageSettings.ApplicationSettings.Contains(USER))
            {
                user = new UserProfile();
                IsolatedStorageSettings.ApplicationSettings.Add(USER, user);
                IsolatedStorageSettings.ApplicationSettings.Save();
            }
            else
            {
                user = (UserProfile)IsolatedStorageSettings.ApplicationSettings[USER];
            }
        }

        #endregion

        /// <summary>
        /// Methods of the page ActualProfilePage
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region Methods

        
        /// <summary>
        /// Reset the Points and the Tasks of the _user
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            MessagePrompt resetMessage = new MessagePrompt
            {
                Title = "Reset your profile",
                Message = "Do you really want to reset your profile ? Only your tasks will be saved.",
                IsCancelVisible = true
            };
            resetMessage.Completed += (me, margs) =>
            {
                if (margs.PopUpResult != PopUpResult.Cancelled)
                {
                    user.Reset();
                    refresh();
                }
            };
            resetMessage.Show();
        }

        
        /// <summary>
        /// Refresh the ProgressBar Enjoyement, the image and the comments according to the Points of the _user
        /// </summary>
        private void refresh()
        {
            int points = user.Life;

            this.txtBlkUserLevel.Text = user.Level.ToString();
            this.txtBlkUserGold.Text = user.Gold.ToString();
            this.XP.Text = String.Format("{0} / {1}", user.Xperience, user.NextXperience);
            this.pgrsXP.Value = user.Xperience * 100 / user.NextXperience;
            this.txtBlkUserLife.Text = String.Format("{0} / {1}", user.Life, user.MaxLife);


            this.pgrsBrEnjoyement.Maximum = user.MaxLife;
            this.pgrsBrEnjoyement.Value = points;

            this.rchTxtBxComments.SelectAll();
            if (points < 10)
            {
                this.CommentAndImage(step1);

            }
            else if (points < 30)
            {
                this.CommentAndImage(step2);
            }
            else if (points < 50)
            {
                this.CommentAndImage(step3);
            }
            else if (points < 70)
            {
                this.CommentAndImage(step4);
            }
            else if (points < 90)
            {
                this.CommentAndImage(step5);
            }
            else
            {
                this.CommentAndImage(step6);
            }

            user.Save();
        }

        /// <summary>
        /// Set the source of the image and the comment
        /// </summary>
        /// <param name="image"></param>
        /// <param name="text"></param>
        private void CommentAndImage(String text)
        {
            this.rchTxtBxComments.Selection.Text = text;
        }

        
        /// <summary>
        /// Refresh the page ActualProfilePage at the loading of the page ActualProfilePage
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            this.refresh();
        }
        /// <summary>
        /// Navigation to the page AwardList.xaml
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void awardBtn_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("/Awards/AwardList.xaml", UriKind.Relative));

        }

        #endregion

    }
}
