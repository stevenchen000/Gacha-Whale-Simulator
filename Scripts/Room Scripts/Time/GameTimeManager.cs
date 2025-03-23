using System;
using EventSystem;
using Godot;

namespace TimeSystem
{
    public partial class GameTimeManager : Node, ISavable
    {
        private static GameTimeManager _instance;

        [Export] public int Year { get; private set; } = 1;
        [Export] public int Month { get; private set; } = 1;
        [Export] public int Week { get; private set; } = 1;
        /// <summary>
        /// Day of the month
        /// 5 = Monday
        /// </summary>
        [Export] public int Day { get; private set; } = 5;

        [Export] public TimeOfDay Time { get; private set; } = TimeOfDay.NIGHT;

        [ExportCategory("Events")]
        [Export] public VoidEvent OnTimeChanged;


        public override void _Ready()
        {
            if(_instance == null)
            {
                _instance = this;
            }
            else
            {
                QueueFree();
            }
        }




        public void ProgressTime()
        {
            UpdateTimeOfDay();
            if(Time == TimeOfDay.MORNING)
                StartNextDay();
            OnTimeChanged.RaiseEvent(this);
        }

        private void StartNextDay()
        {
            UpdateDay();
            UpdateWeek();
            UpdateMonthAndYear();
        }

        private void UpdateTimeOfDay()
        {
            Time = (TimeOfDay)(((int)Time + 1) % 3);
        }

        private void UpdateDay()
        {
            Day++;
            while (Day > 28)
            {
                Month++;
                Day -= 28;
            }
        }

        private void UpdateWeek()
        {
            int newWeek = Day / 7 + 1;
            if(newWeek != Week)
            {
                Week = newWeek;
            }
        }

        private void UpdateMonthAndYear()
        {
            if(Month > 12)
            {
                Month = 1;
                Year++;
            }
        }

        public DayOfWeek GetDayOfWeek()
        {
            return (DayOfWeek)(Day % 7 + 1);
        }

        public bool HasWeekChanged()
        {
            return Day % 7 == 1;
        }

        public bool HasMonthChanged()
        {
            return Day == 1;
        }

        public bool HasYearChanged()
        {
            return Day == 1 && Month == 1;
        }


        /*****************
         * Save and Load
         * ***************/

        private string yearFlag = "time_year";
        private string monthFlag = "time_month";
        private string dayFlag = "time_day";
        private string timeFlag = "time_of_day";
        
        public void Save()
        {
            GameState.SetFlag(timeFlag, (int)Time);
            GameState.SetFlag(dayFlag, Day);
            GameState.SetFlag(monthFlag, Month);
            GameState.SetFlag(yearFlag, Year);
        }

        public void Load()
        {
            int day = GameState.GetFlagAmount(dayFlag);
            if(day != 0)
            {
                Time = (TimeOfDay)GameState.GetFlagAmount(timeFlag);
                Day = GameState.GetFlagAmount(dayFlag);
                Month = GameState.GetFlagAmount(monthFlag);
                Year = GameState.GetFlagAmount(yearFlag);
            }
        }
    }
}
