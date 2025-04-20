using Godot;
using System;

namespace CombatSystem
{
    [GlobalClass]
    public partial class HealthDamageEffect : SkillEffect
    {
        [Export] private float ampConsumptionPercent = 100;
        protected override void _StartEffect(TurnData data, SkillCastData skillCast)
        {
            DealDamage(data, skillCast);
        }

        protected override bool _RunEffect(TurnData data, SkillCastData skillCast, TimeHandler timer)
        {
            return timer.TimeIsUp(delay);
        }

        protected override void _EndEffect(TurnData data, SkillCastData skillCast)
        {
            
        }

        
        private void DealDamage(TurnData data, SkillCastData skillCast)
        {
            var caster = skillCast.Caster;
            var targets = GetTargets(skillCast);
            int damagePerTarget = CalculateDamage(caster, targets.Count);

            caster.Stats.ConsumeAmp(ampConsumptionPercent);
            foreach (var target in targets)
            {
                DealDamageToTarget(data, skillCast, caster, target, damagePerTarget);
            }
        }

        private void DealDamageToTarget(TurnData data, SkillCastData skillCast, BattleCharacter caster, BattleCharacter target, int damage)
        {
            target.Stats.TakeDamage(damage);
            data.AddHpDamage(caster, target, damage);
            
            DamageNumberManager.ShowDamageNumber(target, damage, DamageType.HealthDamage);
        }

        private int CalculateDamage(BattleCharacter caster, int numOfTargets)
        {
            int casterAmp = caster.Stats.GetSlidingStat(StatNames.Amp);
            float percent = ampConsumptionPercent / 100;
            int damage = (int)(casterAmp * percent);

            return damage / numOfTargets;
        }
    }
}
