using Godot;
using SkillSystem;
using System;

namespace CombatSystem
{
    [GlobalClass]
    public partial class ApplyStatusEffect : SkillEffect
    {
        [Export] private TargetType targetType = TargetType.Target;
        [Export] private StatusEffect status;

        protected override void _StartEffect(TurnData data, SkillCastData skillCast)
        {
            var caster = skillCast.Caster;
            var targets = GetTargets(skillCast);

            switch (targetType) {
                case TargetType.Self:
                    caster.AddStatus(caster, status);
                    break;
                case TargetType.Target:
                    foreach (var target in targets)
                    {
                        target.AddStatus(caster, status);
                    }
                    break;
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
