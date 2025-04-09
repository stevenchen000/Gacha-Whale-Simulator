using Godot;
using System;

namespace CombatSystem
{
    [GlobalClass]
    public partial class HealEffect : SkillEffect
    {
        [Export] private StatType statToHeal;
        [Export] private StatType scalingStat;
        [Export] private float potency;
        [Export] private float duration = 0.2f;

        protected override void _StartEffect(TurnData data)
        {
            HealHealth(data);
        }

        protected override bool _RunEffect(TurnData data, TimeHandler timer)
        {
            return timer.TimeIsUp(delay + duration);
        }

        protected override void _EndEffect(TurnData data)
        {
            
        }

        
        private void HealHealth(TurnData data)
        {
            var caster = data.caster;
            var targets = data.targets;

            foreach(var target in targets)
            {
                HealTargets(data, caster, target);
            }
        }

        private void HealTargets(TurnData data, BattleCharacter caster, BattleCharacter target)
        {
            Utils.Print(this, caster);
            Utils.Print(this, caster.Stats);
            Utils.Print(this, caster.Stats.GetStat(scalingStat));
            int casterStat = caster.Stats.GetStat(scalingStat);

            int totalHealing = (int)(casterStat * potency / 100);
            target.BatteryAmp(totalHealing);

            DamageNumberManager.ShowDamageNumber(target, totalHealing, DamageType.Healing);
            Utils.Print(this, $"Healed {scalingStat.StatName}: " + totalHealing);
        }
    }
}
