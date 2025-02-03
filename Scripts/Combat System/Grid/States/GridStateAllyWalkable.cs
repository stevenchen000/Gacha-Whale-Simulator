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
            Space.SetColor(Space.allyColor);
        }

        protected override void RunState(double delta)
        {
            
        }

        protected override StateNode CheckStateChange()
        {
            StateNode result = null;

            if (Space.IsWalkable && Space.PartyIndex == 1)
                result = enemyWalkableState;
            else if(Space.IsWalkable && Space.PartyIndex == 0)
            {
                if (Space.HasSelectedTarget)
                    result = allySelectedSkill;
                else if (Space.CanTarget)
                    result = allyTargetable;
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
