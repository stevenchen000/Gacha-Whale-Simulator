using System;
using Godot;
using StateSystem;

namespace CombatSystem
{
    public partial class GridStateEnemyWalkable : GridStateNode
    {
        [Export] private GridStateNode defaultState;
        [Export] private GridStateNode enemyTargetable;
        [Export] private GridStateNode enemySelectedSkill;
        [Export] private GridStateNode allyWalkableState;

        protected override void OnStateActivated()
        {
            Space.SetColor(Space.enemyColor);
        }

        protected override void RunState(double delta)
        {
            
        }

        protected override StateNode CheckStateChange()
        {
            StateNode result = null;

            if (Space.IsWalkable && Space.PartyIndex == 0)
                result = allyWalkableState;
            else if(Space.PartyIndex == 1)
            {
                if (Space.HasSelectedTarget)
                    result = enemySelectedSkill;
                else if (Space.CanTarget)
                    result = enemyTargetable;
            }
            else if(!Space.IsWalkable)
                result = defaultState;

            return result;
        }

        protected override void OnStateDeactivated()
        {
            
        }
    }
}
