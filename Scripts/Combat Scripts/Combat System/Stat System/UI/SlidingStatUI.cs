using Godot;
using System;

namespace CombatSystem
{
    public partial class SlidingStatUI : StatUI
    {
        [Export] private ProgressBar bar;
        [Export] private bool showMaxStat = true;

        public override void UpdateDisplay()
        {
            int maxStat = statsList.GetStat(stat);
            int currStat = statsList.GetSlidingStat(stat);

            if (statLabel != null)
            {
                if (showMaxStat)
                {
                    statLabel.Text = $"{currStat}/{maxStat}";
                }
                else
                {
                    statLabel.Text = $"{currStat}";
                }
            }

            if (nameLabel != null)
            {
                nameLabel.Text = stat.StatName;
            }

            if (bar != null)
            {
                float percent = (float)currStat / maxStat * 100;
                bar.Value = percent;
            }
        }
    }
}