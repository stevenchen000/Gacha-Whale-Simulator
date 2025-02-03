using System;
using Godot;
using StateSystem;

namespace CombatSystem
{
    public partial class GridStateAllyTargetable : GridStateNode
    {
        [Export] private GridStateNode defaultState;
        [Export] private GridStateNode allySelectedSkill;

        protected override void OnStateActivated()
        {
            Space.SetColor(Space.allyTargetColor);
        }

        protected override void RunState(double delta)
        {
            
        }

        protected override StateNode CheckStateChange()
        {
            StateNode result = null;

            if(Space.CanTarget)
            {
                if (Space.HasSelectedTarget)
                    result = allySelectedSkill;
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
