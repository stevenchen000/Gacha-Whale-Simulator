using System;
using Godot;
using StateSystem;

namespace CombatSystem
{
    public partial class GridStateWalkable : GridStateNode
    {
        [Export] private GridStateNode defaultState;
        [Export] private GridStateNode selectableState;
        [Export] private GridStateNode targetingState;


        protected override void OnStateActivated()
        {
            if (Space.TargetIsEnemy)
                Space.SetColor(Space.enemyColor);
            else
                Space.SetColor(Space.allyColor);
        }

        protected override void RunState(double delta)
        {
            
        }



        protected override StateNode CheckStateChange()
        {
            StateNode result = null;

            if (Space.CanSelect)
                result = selectableState;
            else if (Space.HasSelected)
                result = targetingState;
            else if (!Space.IsWalkable)
                result = defaultState;

            return result;
        }

        protected override void OnStateDeactivated()
        {
            
        }
    }
}
