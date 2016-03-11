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
using System.Collections.Generic;

namespace SuperPotatoesPower
{
    public class AwardCategory
    {
        #region Fields
        public string Name { get; set; }
        public List<Award> Category { get; set; }
        #endregion
    }
}
