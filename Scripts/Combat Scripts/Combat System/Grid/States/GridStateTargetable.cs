using System;
using Godot;
using StateSystem;

namespace CombatSystem
{
    public partial class GridStateTargetable : GridStateNode
    {
        [Export] private GridStateNode defaultState;
        [Export] private GridStateNode allySelectedSkill;

        protected override void OnStateActivated()
        {
            
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
