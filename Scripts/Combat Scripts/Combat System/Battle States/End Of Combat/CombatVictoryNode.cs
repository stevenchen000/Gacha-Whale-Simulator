using System;
using Godot;
using StateSystem;

namespace CombatSystem
{
    public partial class CombatVictoryNode : CombatStateNode
    {
        [Export] private CombatStateNode rewardNode;

        protected override void OnStateActivated()
        {
            base.OnStateActivated();
        }

        protected override void RunState(double delta)
        {
            
        }

        protected override StateNode CheckStateChange()
        {
            StateNode result = null;
            if (timeInState > 2) result = rewardNode;
            return result;
        }

        protected override void OnStateDeactivated()
        {
            
        }
    }
}
