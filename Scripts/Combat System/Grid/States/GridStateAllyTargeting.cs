using System;
using Godot;
using StateSystem;

namespace CombatSystem
{
    public partial class GridStateAllyTargeting : GridStateNode
    {
        [Export] private GridStateNode allyTargetable;

        protected override void OnStateActivated()
        {
            Space.SetColor(Space.allyAttackColor);
        }

        protected override void RunState(double delta)
        {
            
        }

        protected override StateNode CheckStateChange()
        {
            StateNode result = null;

            if (!Space.HasSelectedTarget)
                result = allyTargetable;
    
            return result;
        }

        protected override void OnStateDeactivated()
        {
            
        }
    }
}
