using System;
using Godot;
using StateSystem;

namespace CombatSystem
{
    public partial class GridStateTargeting : GridStateNode
    {
        [Export] private GridStateNode defaultState;

        protected override void OnStateActivated()
        {
            var targetData = Grid.CurrentTargetingData;
            var targetType = targetData.Skill.Skill.targetType;
            if(targetType == TargetType.Enemy)
                Space.SetColor(Space.attackColor);
            else if(targetType == TargetType.Ally)
                Space.SetColor(Space.healColor);
        }

        protected override void RunState(double delta)
        {
            
        }

        protected override StateNode CheckStateChange()
        {
            StateNode result = null;

            if (!Space.HasSelected)
                result = defaultState;
    
            return result;
        }

        protected override void OnStateDeactivated()
        {
            
        }
    }
}
