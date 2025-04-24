using Godot;
using SkillSystem;
using System;

namespace CombatSystem
{
    [Tool]
    [GlobalClass]
    public partial class DelayTurnEffect : SkillEffect
    {
        [Export] private int numOfTurns = 1;

        protected override void _StartEffect(TurnData data, SkillCastData skillCast)
        {
            var targets = GetTargets(skillCast);

            foreach (var target in targets)
            {
                target.Delay(numOfTurns);
            }
        }

        protected override bool _RunEffect(TurnData data, SkillCastData skillCast, TimeHandler timer)
        {
            return timer.TimeIsUp(delay);
        }

        protected override void _EndEffect(TurnData data, SkillCastData skillCast)
        {
            
        }
    }
}
