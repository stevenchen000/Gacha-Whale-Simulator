using System;
using Godot;
using StateSystem;

namespace CombatSystem
{
    public partial class CombatActivatingNode : CombatStateNode
    {
        [Export] private CombatStateNode turnNode;

        protected override void OnStateActivated()
        {
            Utils.Print(this, "Combat has started!");
        }

        protected override void RunState(double delta)
        {
            
        }

        protected override StateNode CheckStateChange()
        {
            StateNode result = null;

            if (timeInState > 1) result = turnNode;
            
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
