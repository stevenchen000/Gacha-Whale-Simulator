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
            GD.Print("Combat has started!");
            ChangeState(turnNode);
        }

        protected override void OnStateDeactivated()
        {
            
        }
    }
}
