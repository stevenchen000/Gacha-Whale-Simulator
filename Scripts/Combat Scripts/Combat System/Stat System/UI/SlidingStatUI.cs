using Godot;
using System;

namespace CombatSystem
{
    public partial class SlidingStatUI : StatUI
    {
        [Export] private ProgressBar bar;
        [Export] private bool showMaxStat = true;
        private double displayedValue = 0;
        private double currValue = 0;
        [Export] private double changeSpeed = 10;


        public override void _Process(double delta)
        {
            if (displayedValue != currValue)
            {
                UpdateDisplayedValue(delta);
                UpdateDisplay();
            }
        }

        private void UpdateDisplayedValue(double delta)
        {
            displayedValue = Mathf.Lerp(displayedValue, currValue, changeSpeed * delta);
            if (GetDifference() < 0.5) displayedValue = currValue;
        }

        private double GetDifference()
        {
            return Mathf.Abs(displayedValue - currValue);
        }



        public override void UpdateDisplay()
        {
            int maxStat = statsList.GetStat(stat);
            currValue = statsList.GetSlidingStat(stat);

            if (statLabel != null)
            {
                if (showMaxStat)
                {
                    statLabel.Text = $"{(int)displayedValue}/{maxStat}";
                }
                else
                {
                    statLabel.Text = $"{(int)displayedValue}";
                }
            }

            if (nameLabel != null)
            {
                nameLabel.Text = stat.StatName;
            }

            if (bar != null)
            {
                float percent = (float)displayedValue / maxStat * 100;
                bar.Value = percent;
            }
        }


    }
}