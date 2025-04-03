using System;
using Godot;
using StateSystem;

namespace CombatSystem
{
    public partial class GridStateDefault : GridStateNode
    {
        [Export] private GridStateNode walkableState;
        [Export] private GridStateNode selectableState;
        [Export] private GridStateNode targetingState;


        protected override void OnStateActivated()
        {
            Space.SetColor(Space.defaultColor);
        }

        protected override void RunState(double delta)
        {
            
        }

        protected override StateNode CheckStateChange()
        {
            StateNode result = null;
            
            if (Space.IsWalkable) result = walkableState;
            else if (Space.CanSelect) result = selectableState;
            else if (Space.HasSelected) result = targetingState;
            
            return result;
        }

        protected override void OnStateDeactivated()
        {
            
        }
    }
}
