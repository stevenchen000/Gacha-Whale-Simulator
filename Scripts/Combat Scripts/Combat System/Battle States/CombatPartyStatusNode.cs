using System;
using Godot;
using StateSystem;

namespace CombatSystem
{
    public partial class CombatPartyStatusNode : CombatStateNode
    {
        [Export] private CombatStateNode victoryCheckNode;

        protected override void OnStateActivated()
        {
            base.OnStateActivated();
            battle.UpdateLivingStatus();
        }

        protected override void RunState(double delta)
        {
            
        }

        protected override StateNode CheckStateChange()
        {
            StateNode result = null;
            result = victoryCheckNode;
            return result;
        }

        protected override void OnStateDeactivated()
        {
            
        }
    }
}
