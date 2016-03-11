using System;
using System.Net;
using System.Linq;
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
    public static class TaskBoxManager
    {
        #region Constant texts
        private const string PASTDAYSTEXT = "You forgot to";

        private const string DAYTEXT = "Urgent ! You have to";
        private const string TOMORROWTEXT = "Take care, don't forget to";
        private const string NEXTWEEKTEXT = "If you want to take the lead you should";
        private const string NEXTMONTHTEXT = "It's alright, you have time but you could";

        private const string TODAYTITLE = "Today";
        private const string THISDAYTITLE = "This Day";
        private const string TOMORROWTITLE = "Tomorrow";
        private const string NEXTDAYTITLE = "Next Day";
        private const string NEXTWEEKTITLE = "Next Week";
        private const string NEXTMONTHTITLE = "Next Month";

        private const int DAYDEADLINE = 1;
        private const int NEXTDAYDEADLINE = 2;
        private const int NEXTWEEKDEADLINE = 7;
        private const int NEXTMONTHDEADLINE = 1;
        #endregion

        public static TaskBox getUnvalidatedTaskBox(IEnumerable<Task> taskList, DateTime day)
        {
            DateTime deadline = new DateTime(day.Year, day.Month, day.Day);

            IEnumerable<Task> tasks = from task in taskList 
                                      where (!task.IsValidated && task.StartDate < day.AddDays(1) && task.Deadline < deadline) 
                                      select task;
            ObservableCollection<Task> observableTasks = new ObservableCollection<Task>(tasks);
            
            #region Constructing the TaskBox

            // return null if there is no task in the taskBox
            if (observableTasks.Count == 0)
            {
                return null;
            }

            return new TaskBox
            {
                Tasks = observableTasks,
                Date = PASTDAYSTEXT,
                DescriptionColor = "Red",
                ActionText = "Report",
                ActionEvent = "Modify_Click"
            };
            #endregion
        }
        
        public static TaskBox getTaskBox(IEnumerable<Task> taskList, DateTime day, DeadlineTime deadlineTime)
        {
            DateTime deadlineStartDate = new DateTime();
            DateTime deadlineDate = new DateTime();
            string date = "";
            string dateTextBox = "";
            DateTime dayWithoutHours = new DateTime(day.Year, day.Month, day.Day);
            
            #region Getting date of deadline and text corresponding
            switch (deadlineTime)
            {
                case DeadlineTime.Today:
                    if (dayWithoutHours == DateTime.Today)
                    {
                        date = TODAYTITLE;
                    }
                    else
                    {
                        date = THISDAYTITLE;
                    }
                    deadlineStartDate = day;
                    deadlineDate = day.AddDays(DAYDEADLINE);
                    dateTextBox = DAYTEXT;
                    break;
                case DeadlineTime.Tomorrow:
                    if (dayWithoutHours == DateTime.Today)
                    {
                        date = TOMORROWTITLE;
                    }
                    else
                    {
                        date = NEXTDAYTITLE;
                    }
                    deadlineStartDate = day.AddDays(DAYDEADLINE);
                    deadlineDate = day.AddDays(NEXTDAYDEADLINE);
                    dateTextBox = TOMORROWTEXT;
                    break;
                case DeadlineTime.NextWeek:
                    date = NEXTWEEKTITLE;
                    deadlineStartDate = day.AddDays(NEXTDAYDEADLINE);
                    deadlineDate = day.AddDays(NEXTWEEKDEADLINE);
                    dateTextBox = NEXTWEEKTEXT;
                    break;
                case DeadlineTime.NextMonth:
                    date = NEXTMONTHTITLE;
                    deadlineStartDate = day.AddDays(NEXTWEEKDEADLINE);
                    deadlineDate = day.AddMonths(NEXTMONTHDEADLINE);
                    dateTextBox = NEXTMONTHTEXT;
                    break;
                default:
                    break;
            }

            IEnumerable<Task> tasks = from task in taskList 
                                      where (task.StartDate < day.AddDays(1) && task.Deadline >= deadlineStartDate && task.Deadline < deadlineDate) 
                                      select task;
            ObservableCollection<Task> observableTasks = new ObservableCollection<Task>(tasks);

            #endregion

            #region Constructing the TaskBox

            // return null if there is no task in the taskBox
            if (observableTasks.Count == 0)
            {
                return null;
            }

            return new TaskBox
            {
                Tasks = observableTasks,
                Date = date,
                DateDescription = dateTextBox,
                DescriptionColor = "White",
                ActionText = "Modify",
                ActionEvent = "Modify_Click"
            };
            #endregion
        }

        public static ObservableCollection<TaskBox> getTaskBoxes(List<Task> taskList, DateTime day)
        {
            if (taskList.Count != 0)
            {
                IEnumerable<Task> taskListEnumerable = taskList.AsEnumerable<Task>();
                ObservableCollection<TaskBox> taskBoxes = new ObservableCollection<TaskBox>();

                taskBoxes.Add(TaskBoxManager.getUnvalidatedTaskBox(taskList, day));
                taskBoxes.Add(TaskBoxManager.getTaskBox(taskList, day, DeadlineTime.Today));
                taskBoxes.Add(TaskBoxManager.getTaskBox(taskList, day, DeadlineTime.Tomorrow));
                taskBoxes.Add(TaskBoxManager.getTaskBox(taskList, day, DeadlineTime.NextWeek));
                taskBoxes.Add(TaskBoxManager.getTaskBox(taskList, day, DeadlineTime.NextMonth));

                return taskBoxes;
            }

            return null;
        }
    }
}
