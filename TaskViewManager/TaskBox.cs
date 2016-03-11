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

namespace SuperPotatoesPower
{
    public class TaskBox
    {
        public ObservableCollection<Task> Tasks { get; set; }

        public string DateDescription { get; set; }
        public string Date { get; set; }
        public string DescriptionColor { get; set; }
        public string ActionText { get; set; }
        public string ActionEvent { get; set; }
    }
}
