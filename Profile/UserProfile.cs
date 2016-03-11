using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.IsolatedStorage;

namespace SuperPotatoesPower
{
    public class UserProfile
    {
        public const string USER = "_user";

        public int Level { get; set; }

        public int Points { get; set; }

        public int Life { get; set; }

        public int MaxLife { get; set; }

        public int Xperience { get; set; }

        public int NextXperience { get; set; }

        public int Gold { get; set; }

        public string Name { get; set; }

        public List<Task> TaskList { get; set; }

        public List<Award> CustomAwards { get; set; }

        //Save the UserProfile from the storage
        public void Save()
        {
            IsolatedStorageSettings.ApplicationSettings[UserProfile.USER] = this;
            IsolatedStorageSettings.ApplicationSettings.Save();
        }

        //Load the UserProfile from the storage
        public UserProfile Load()
        {
            return (UserProfile)IsolatedStorageSettings.ApplicationSettings[UserProfile.USER];
        }

        public UserProfile()
        {
            this.Reset();
            this.TaskList = new List<Task>
            {
                new Task{ Title="Create a task !", Points = 1, EstimatedTime = TimeSpan.FromMinutes(50), Deadline = DateTime.Today.AddDays(1)}
            };
        }

        //This method allows to refresh the UserProfile.
        //It send -1 if the User is dead, 1 if he levels up and 0 if none of these happens.
        public int refreshUser(int points, int xperience, int life, int gold)
        {
            this.Gold += this.Level * gold;
            this.Points += this.Level * points;
            this.Xperience += this.Level * xperience;
            this.Life += life;

            if (this.Life + life <= 0)
            {
                this.Points = 0;
                this.Gold = 0;
                if (this.Level > 1)
                {
                    this.Level--;
                    this.NextXperience += -3 * (this.Level - 1) * (this.Level - 1);
                }
                this.Xperience = 0;
                this.Life = this.MaxLife;
                this.Save();
                return (-1);
            }

            if (this.Xperience + xperience >= this.NextXperience)
            {
                this.Gold += this.Level;
                this.Level++;
                this.Xperience = this.Xperience + xperience - this.NextXperience;
                this.NextXperience += 3 * this.Level * this.Level;
                this.MaxLife += 10;
                this.Life = this.MaxLife;
                this.Save();
                return 1;
            }

            /*if (this.Xperience + xperience < this.NextXperience)
            {
                //this.Gold += this.Level;
                this.Level--;
                this.Xperience = this.Xperience + xperience - this.NextXperience;
                this.NextXperience += 3 * this.Level * this.Level;
                this.MaxLife -= 10;
                this.Life = this.MaxLife;
                this.SaveUserProfile();
                return -2;
            }*/

            this.Save();

            return 0;
        }

        //This method reset the userProfile
        public void Reset()
        {
            this.Points = 56;
            this.Name = "Your Name";
            this.Xperience = 30;
            this.NextXperience = 50;
            this.Level = 1;
            this.Life = 50;
            this.MaxLife = 50;
            this.Gold = 35;
            this.CustomAwards = new List<Award>();
        }

    }
}
