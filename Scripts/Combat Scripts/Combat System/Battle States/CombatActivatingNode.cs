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
            
        }

        protected override void RunState(double delta)
        {
            Utils.Print(this, "Combat has started!");
        }

        protected override StateNode CheckStateChange()
        {
            return turnNode;
        }

        protected override void OnStateDeactivated()
        {
            
        }
    }
}
