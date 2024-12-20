using System;
using Godot;
using StateSystem;

namespace CombatSystem
{
    public partial class CombatAnimationNode : CombatStateNode
    {
        [Export] private CombatStateNode damageCalculationNode;

        protected override void OnStateActivated()
        {
            GD.Print("Pretend the animation played out");
        }

        protected override void RunState(double delta)
        {
            var castData = battle.castData;
            var skill = castData.skill;
            bool finishedCast = skill.PlayAnimation(castData, delta);

            if (finishedCast)
            {
                ChangeState(damageCalculationNode);
            }
        }

        protected override void OnStateDeactivated()
        {
            
        }
    }
}
