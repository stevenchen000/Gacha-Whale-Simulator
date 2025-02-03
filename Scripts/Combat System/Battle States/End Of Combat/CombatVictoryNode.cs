using System;
using Godot;
using StateSystem;

namespace CombatSystem
{
    public partial class CombatVictoryNode : CombatStateNode
    {
        

        protected override void OnStateActivated()
        {
            base.OnStateActivated();
            Utils.Print(this, "You win!");
        }

        protected override void RunState(double delta)
        {
            
        }

        protected override StateNode CheckStateChange()
        {
            StateNode result = null;

            return result;
        }

        protected override void OnStateDeactivated()
        {
            
        }
    }
}
