using System;
using Godot;
using StateSystem;

namespace CombatSystem
{
    public partial class CombatFinishedNode : CombatStateNode
    {
        

        protected override void OnStateActivated()
        {
            //check victor
        }

        protected override void RunState(double delta)
        {
            //play victory/defeat animation
        }

        protected override void OnStateDeactivated()
        {
            
        }
    }
}
