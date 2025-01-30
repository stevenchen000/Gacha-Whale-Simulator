using System;
using Godot;
using StateSystem;

namespace CombatSystem
{
    public partial class GridStateAllyWalkable : GridStateNode
    {
        [Export] private GridStateNode defaultState;
        [Export] private GridStateNode allyTargetable;
        [Export] private GridStateNode allySelectedSkill;
        [Export] private GridStateNode enemyWalkableState;

        protected override void OnStateActivated()
        {
            space.SetColor(space.allyColor);
        }

        protected override void RunState(double delta)
        {
            
        }

        protected override StateNode CheckStateChange()
        {
            StateNode result = null;

            if (space.IsWalkable && space.PartyIndex == 1)
                result = enemyWalkableState;
            else if(space.PartyIndex == 0)
            {
                if (space.HasSelectedTarget)
                    result = allySelectedSkill;
                else if (space.CanTarget)
                    result = allyTargetable;
            }
            else if(!space.IsWalkable)
                result = defaultState;

            return result;
        }

        protected override void OnStateDeactivated()
        {
            
        }
    }
}
