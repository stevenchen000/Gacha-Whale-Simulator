using Godot;
using System;

namespace CombatSystem
{
    public partial class StatUI : Node
    {
        [Export] protected StatType stat;
        [Export] protected Label nameLabel;
        [Export] protected Label statLabel;
        [Export] protected Node statContainer;
        protected IStatContainer statsList;

        public override void _Ready()
        {
            if(statContainer == null)
            {
                Utils.Print(this, "Error: Stat Container null");
            }
            else if(!(statContainer is IStatContainer))
            {
                Utils.Print(this, "Error: Not a Stat Container");
            }
            else if(stat == null)
            {
                Utils.Print(this, "Error: StatType is null");
            }
            else
            {
                statsList = (IStatContainer)statContainer;
            }

            CallDeferred(MethodName.UpdateDisplay);
        }

        public override void _Notification(int what)
        {
            if(what == NotificationPredelete)
            {
                statContainer = null;
                statsList = null;
            }
        }

        public virtual void UpdateDisplay()
        {
            int statAmount = statsList.GetStat(stat);
            
            if(statLabel != null)
            {
                statLabel.Text = statAmount.ToString();
            }

            if(nameLabel != null)
            {
                nameLabel.Text = stat.StatName;
            }
        }
    }
}