using Godot;
using System;

namespace CombatSystem
{
    [Tool]
    [GlobalClass]
    public partial class HealEffect : SkillEffect
    {
        [Export] private StatType statToHeal;
        [Export] private StatType scalingStat;
        [Export] private float potency;
        [Export] private float duration = 0.2f;

        protected override void _StartEffect(TurnData data, SkillCastData skillCast)
        {
            HealHealth(data, skillCast);
        }

        protected override bool _RunEffect(TurnData data, SkillCastData skillCast, TimeHandler timer)
        {
            return timer.TimeIsUp(delay + duration);
        }

        protected override void _EndEffect(TurnData data, SkillCastData skillCast)
        {
            
        }

        
        private void HealHealth(TurnData data, SkillCastData skillCast)
        {
            var caster = skillCast.Caster;
            var targets = GetTargets(skillCast);

            foreach (var target in targets)
            {
                HealTargets(data, skillCast, caster, target);
            }
        }

        private void HealTargets(TurnData data, SkillCastData skillCast, BattleCharacter caster, BattleCharacter target)
        {
            int casterStat = caster.Stats.GetStat(scalingStat);

            int totalHealing = (int)(casterStat * potency / 100);
            target.BatteryAmp(totalHealing);

            DamageNumberManager.ShowDamageNumber(target, totalHealing, DamageType.Healing);
        }
    }
}
