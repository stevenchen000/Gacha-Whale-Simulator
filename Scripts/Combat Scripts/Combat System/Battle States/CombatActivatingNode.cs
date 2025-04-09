
using Godot;
using StateSystem;
using Godot.Collections;

namespace CombatSystem
{
    public partial class CombatActivatingNode : CombatStateNode
    {
        [Export] private CombatStateNode turnNode;
        private Array<GridSpace> spaces;
        private int index = 0;
        private TimeHandler time;
        [Export] private double gridActivateInterval = 0.1;

        protected override void OnStateActivated()
        {
            var grid = battle.Grid;
            spaces = new Array<GridSpace>();
            spaces.AddRange(grid.GetSortedListByDiagonal());
            time = new TimeHandler();
        }

        protected override void RunState(double delta)
        {
            if (timeInState < 1) return;

            time.Tick(delta);
            int newIndex = time.IntervalReached(gridActivateInterval);
            if(newIndex >= index && newIndex < spaces.Count)
            {
                for(int i = index; i <= newIndex && i < spaces.Count; i++)
                {
                    var space = spaces[i];
                    space.PlayAppearAnimation();
                }
                index = newIndex;
            }
        }

        protected override StateNode CheckStateChange()
        {
            StateNode result = null;

            if (index == spaces.Count - 1) result = turnNode;
            
            return result;
        }

        protected override void OnStateDeactivated()
        {
            SetInitialAmp();
        }

        private void SetInitialAmp()
        {
            var fighters = battle.State.GetAllLivingCharacters();

            foreach(var fighter in fighters)
            {
                int initialValue = fighter.Stats.GetStat(StatNames.Spirit);
                fighter.Stats.SetSlidingStat(StatNames.Amp, initialValue);
            }
        }
    }
}
