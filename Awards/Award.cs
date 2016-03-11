using System.Windows;

namespace SuperPotatoesPower
{
    public class Award
    {
        #region Fields
        public string Name { get; set; }
        public int Golds { get; set; }
        public bool IsUnlocked { get; set; }
        public AwardCategory Category { get; set; }

        public Visibility IsVisible
        {
            get
            {
                return IsUnlocked ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        #endregion

        public Award()
        {
            IsUnlocked = false;
        }

    }
}
