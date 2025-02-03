using System;
using Godot;
using StateSystem;

namespace CombatSystem
{
    public partial class _CombatNodeTemplate : CombatStateNode
    {
        

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

            return result;
        }

        protected override void OnStateDeactivated()
        {
            
        }
    }
}
