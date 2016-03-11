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

namespace SuperPotatoesPower
{
    public class Task
    {
        #region Properties
        /* --- Modifiable by _user --- */
        public string Title { get; set; }

        public bool IsRepeated { get; set; }
        public RepetitionTime RepetitionTime { get; set; }
        
        private DateTime deadline;
        public DateTime Deadline
        {
            get
            {
                return this.GetDateWithRepetition(deadline);
            }

            set
            {
                deadline = value;
            }
        }
        public string DeadlineString
        {
            get
            {
                return string.Format("{0}/{1} {2} ", Deadline.Month, Deadline.Day, Deadline.ToShortTimeString());
            }
        }

        private DateTime startDate;
        public DateTime StartDate
        {
            get
            {
                return this.GetDateWithRepetition(startDate);
            }
            set
            {
                startDate = value;
            }
        }

        public int Priority { get; set; }

        public TimeSpan EstimatedTime { get; set; }
        public string EstimatedTimeString
        {
            get
            {
                string result = "";
                if (EstimatedTime.Hours != 0)
                {
                    result = string.Format("{0} hrs", EstimatedTime.Hours);
                }
                if (EstimatedTime.Minutes != 0)
                {
                    result = string.Format("{0} {1} min", result, EstimatedTime.Minutes);
                }
                return result;
            }
        }

        /* --- Not modifiable in Modification Page by _user --- */
        public int Points { get; set; }

        public int RepetitionIndex { get; set; }

        private bool isValidated;
        public bool IsValidated 
        {
            get
            {
                return isValidated;
            }
            set
            {
                if (IsRepeated)
                {
                    if (value)
                        RepetitionIndex++;
                    else
                        RepetitionIndex--;
                }
                else
                {
                    isValidated = value;
                }
            }
        }

        #endregion

        #region Constructor
        // Default Task
        public Task()
        {
            Title = "Task";
            Deadline = DateTime.Now.AddDays(1); // For next day
            Priority = 1; // Best Priority
            EstimatedTime = new TimeSpan(1, 0, 0); // Last 1 hour
            StartDate = DateTime.Now; // Can begin now
            IsRepeated = false;

            RepetitionIndex = 0;
            IsValidated = false;
        }
        #endregion

        #region Methods

        /// <summary>
        /// Set the task repetition times
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public DateTime GetDateWithRepetition(DateTime date)
        {
            switch (RepetitionTime)
            {
                case RepetitionTime.None:
                    return date;
                case RepetitionTime.Day:
                    return date.AddDays(RepetitionIndex);
                case RepetitionTime.Week:
                    return date.AddDays(7 * RepetitionIndex);
                case RepetitionTime.Month:
                    return date.AddMonths(RepetitionIndex);
                case RepetitionTime.Year:
                    return date.AddYears(RepetitionIndex);
                default:
                    return date;
            }
        }

        #endregion
    }
}