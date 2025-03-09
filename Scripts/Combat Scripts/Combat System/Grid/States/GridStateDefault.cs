using System;
using Godot;
using StateSystem;

namespace CombatSystem
{
    public partial class GridStateDefault : GridStateNode
    {
        [Export] private GridStateNode allyWalkableState;
        [Export] private GridStateNode allyTargetableState;
        [Export] private GridStateNode allyTargetingState;
        [Export] private GridStateNode enemyWalkableState;


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
            
            if (Space.IsWalkable)
            {
                if (Space.PartyIndex == 0)
                    result = allyWalkableState;
                else if(Space.PartyIndex == 1)
                    result = enemyWalkableState;
            }
            else if (Space.CanTarget)
            {
                result = allyTargetableState;
            }
            else if (Space.HasSelectedTarget)
            {
                result = allyTargetingState;
            }

            return result;
        }

        protected override void OnStateDeactivated()
        {
            
        }
    }
}
