using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using System.Collections.Generic;

namespace SuperPotatoesPower
{
    public struct Awards
    {
        /// <summary>
        /// All the categories genres
        /// </summary>
        #region CategoriesGenre

        #region Sport
        public static readonly List<Award> Sport = new List<Award>
            {               
               new Award{Name = "Participate at a Wheelchair Racing", Golds = 90} ,
               new Award{Name = "Do Parkour in the kitchen", Golds = 80} ,
               new Award{Name = "Go to Rollercoasters with your friends", Golds = 60} ,
               new Award{Name = "Play Basketball with your sister's doll", Golds = 40} ,
               new Award{Name = "Play Football with your family", Golds = 40} ,
               new Award{Name = "Play Paintball with your neighbours", Golds = 60} ,
               new Award{Name = "Play Bowling during the next party", Golds = 70} ,
               new Award{Name = "Play LaserTag with you dog", Golds = 60} ,
               new Award{Name = "Play Quidditch with your imaginary friends", Golds = 100}
            };
        #endregion

        #region VideoGames
        public static readonly List<Award> VideoGames = new List<Award>
        {
            new Award{Name = "A Card Game", Golds = 20} ,
            new Award{Name = "An Action Game", Golds = 50} ,
            new Award{Name = "An Action Game", Golds = 50} ,
            new Award{Name = "An Adventure Game", Golds = 50} ,
            new Award{Name = "An Action-Adventure Game", Golds = 50} ,
            new Award{Name = "A Role Play Game", Golds = 50} ,
            new Award{Name = "A Reflection Game", Golds = 50} ,
            new Award{Name = "A Simulation Game", Golds = 50} ,
            new Award{Name = "A Strategy Game", Golds = 50}
        };
        #endregion

        #region Reading
        public static readonly List<Award> Reading = new List<Award>
        {
            new Award{Name = "SciFi book", Golds = 30} ,
            new Award{Name = "Drama book", Golds = 50} ,
            new Award{Name = "Romance book", Golds = 40} ,
            new Award{Name = "Horror book", Golds = 40} ,
            new Award{Name = "Science book", Golds = 10} ,
            new Award{Name = "History book", Golds = 10} ,
            new Award{Name = "Math book", Golds = 10} ,
            new Award{Name = "Poetry book", Golds = 40} ,
            new Award{Name = "Comics book", Golds = 50} ,
            new Award{Name = "Art book", Golds = 20} ,
            new Award{Name = "Cook book", Golds = 10} ,
            new Award{Name = "Newspaper book", Golds = 10} ,
            new Award{Name = "Biography book", Golds = 20} ,
            new Award{Name = "Fantasy book", Golds = 40}
        };
        #endregion

        #region Music
        public static readonly List<Award> Music = new List<Award>
        {   
             new Award{Name = "African vibes", Golds = 30} ,
             new Award{Name = "AvantGarde noise", Golds = 50} ,
             new Award{Name = "Blues trip", Golds = 30} ,
             new Award{Name = "Carribean rythm", Golds = 30} ,
             new Award{Name = "Country rythm", Golds = 40} ,
             new Award{Name = "Asian sound", Golds = 40} ,
             new Award{Name = "Electronic trip", Golds = 20} ,
             new Award{Name = "HipHop vibes", Golds = 40} ,
             new Award{Name = "Jazz trip", Golds = 30} ,
             new Award{Name = "Pop music", Golds = 20} ,
             new Award{Name = "RnB rythm", Golds = 40} ,
             new Award{Name = "Rock save the rythm", Golds = 20}
        };
        #endregion

        #region Film
        public static readonly List<Award> Film = new List<Award>
        {
            new Award{Name = "Action reported movements", Golds = 50} ,
            new Award{Name = "Animation film", Golds = 60} ,
            new Award{Name = "Martial Arts frames", Golds = 50} ,
            new Award{Name = "Adventure time", Golds = 50} ,
            new Award{Name = "Biopic zzzzz", Golds = 30} ,
            new Award{Name = "Bollywood sings the song of its people", Golds = 40} ,
            new Award{Name = "Comedy trip", Golds = 40} ,
            new Award{Name = "Musical film * teenagers strike again", Golds = 30} ,
            new Award{Name = "Mystery mysterious film", Golds = 40} ,
            new Award{Name = "Romance for else in lack of love", Golds = 40} ,
            new Award{Name = "SciFi film * provide by Spielberg", Golds = 50} ,
            new Award{Name = "Sport muscle and epinephrine", Golds = 50} ,
            new Award{Name = "Thriller film", Golds = 50} ,
            new Award{Name = "Western with good and bad guys", Golds = 50},
            new Award{Name = "Watch one episode of your favorite serie", Golds = 80}
        };
        #endregion

        #region Sweets
        public static readonly List<Award> Sweets = new List<Award>
        {
            new Award{Name = "Lollipop", Golds = 10} ,
            new Award{Name = "Chocolate", Golds = 20} ,
            new Award{Name = "Chocolate bar", Golds = 60} ,
            new Award{Name = "No matter : sweets", Golds = 10}
        };
        #endregion

        #region Partying
        public static readonly List<Award> Partying = new List<Award>
        {
            new Award{Name = "Birthday party", Golds = 100} ,
            new Award{Name = "Surprise party * with lot of surprise !", Golds = 150} ,
            new Award{Name = "Dance party", Golds = 150} ,
            new Award{Name = "Costume - Take a suit !", Golds = 120} ,
            new Award{Name = "Teenagers * puberty srike again", Golds = 90} ,
            new Award{Name = "Singles and forever alone people party", Golds = 80} ,
            new Award{Name = "Graduation party * Oh my God I'm graduated", Golds = 100} ,
            new Award{Name = "Pre party * because if not it will be boring", Golds = 80} ,
            new Award{Name = "After party * because party is not enough", Golds = 80}
        };
        #endregion

        #region Food

        public static readonly List<Award> Food = new List<Award>
        {
            new Award{Name = "Alcoholic drinks * what's up Mr Cop ?", Golds = 70} ,
            new Award{Name = "Cookies * Everybody loves cookies", Golds = 10} ,
            new Award{Name = "Desserts, with lot of desserts on its", Golds = 30} ,
            new Award{Name = "FastFoods * come in diabet, I challenge you", Golds = 60} ,
            new Award{Name = "IceCream * because summer time is not over", Golds = 30} ,
            new Award{Name = "Pizza * now it's a vegetable", Golds = 40} ,
            new Award{Name = "Sandwiches * Eating ? Ain't nobody have time for that", Golds = 20}
        };

        #endregion

        #region UserCategory
        public static List<Award> UserCategory = new List<Award>
        {

        };
        #endregion
        #endregion

        /// <summary>
        /// All the categories
        /// </summary>
        #region Categories
        public static readonly ObservableCollection<AwardCategory> Categories = new ObservableCollection<AwardCategory>
        {
            new AwardCategory
            {Name = "Sport",
                Category = Sport
            },
            new AwardCategory{
                Name = "Video Games",
                Category = VideoGames
            },
            new AwardCategory{
                Name = "Reading",
                Category = Reading
            },
            new AwardCategory{
                Name = "Music",
                Category = Music
            },
            new AwardCategory{
                Name = "Film",
                Category = Film
            },
            new AwardCategory{
                Name = "Sweets",
                Category = Sweets
            },
            new AwardCategory{
                Name = "Partying",
                Category = Partying
            },
            new AwardCategory{
                Name = "Food",
                Category = Food
            }
        };
        #endregion
    }
}
