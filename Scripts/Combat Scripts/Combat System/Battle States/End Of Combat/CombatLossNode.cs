using System;
using Godot;
using StateSystem;

namespace CombatSystem
{
    public partial class CombatLossNode : CombatStateNode
    {
        

        protected override void OnStateActivated()
        {
            base.OnStateActivated();
            Utils.Print(this, "Game Over");
        }

        protected override void RunState(double delta)
        {
            if(timeInState > 5)
            {
                battle.ReturnToCombatMenu();
            }
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
