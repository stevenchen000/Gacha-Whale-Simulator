using Godot;
using System;

namespace CombatSystem
{
    [GlobalClass]
    public partial class HealthDamageEffect : SkillEffect
    {
        [Export] private float ampConsumptionPercent = 100;
        protected override void _StartEffect(TurnData data)
        {
            DealDamage(data);
        }

        protected override bool _RunEffect(TurnData data, TimeHandler timer)
        {
            return timer.TimeIsUp(delay);
        }

        protected override void _EndEffect(TurnData data)
        {
            
        }

        
        private void DealDamage(TurnData data)
        {
            var caster = data.caster;
            var targets = data.targets;

            foreach(var target in targets)
            {
                DealDamageToTarget(data, caster, target);
            }
        }

        private void DealDamageToTarget(TurnData data, BattleCharacter caster, BattleCharacter target)
        {
            int casterAmp = caster.Stats.GetSlidingStat(StatNames.Amp);
            float percent = ampConsumptionPercent / 100;
            int damage = (int)(casterAmp * percent);

            target.Stats.TakeDamage(damage);
            caster.Stats.ConsumeAmp(ampConsumptionPercent);
            data.AddHpDamage(caster, target, casterAmp);
            
            DamageNumberManager.ShowDamageNumber(target, damage, DamageType.HealthDamage);
        }
    }
}
