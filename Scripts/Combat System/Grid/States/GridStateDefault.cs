using System;
using Godot;
using StateSystem;

namespace CombatSystem
{
    public partial class GridStateDefault : GridStateNode
    {
        [Export] private GridStateNode allyWalkableState;
        [Export] private GridStateNode enemyWalkableState;

        protected override void OnStateActivated()
        {
            space.SetColor(space.defaultColor);
        }

        protected override void RunState(double delta)
        {
            
        }

        protected override StateNode CheckStateChange()
        {
            StateNode result = null;
            
            if (space.IsWalkable)
            {
                if (space.PartyIndex == 0)
                    result = allyWalkableState;
                else if(space.PartyIndex == 1)
                    result = enemyWalkableState;
            }

            return result;
        }

        protected override void OnStateDeactivated()
        {
            
        }
    }
}
