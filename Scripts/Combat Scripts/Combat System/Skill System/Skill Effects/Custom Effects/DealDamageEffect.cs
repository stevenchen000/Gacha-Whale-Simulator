using Godot;
using System;

namespace CombatSystem
{
    [GlobalClass]
    public partial class DealDamageEffect : SkillEffect
    {
        [Export] private float potency;
        [Export] private float duration = 0.2f;

        protected override void _StartEffect(TurnData data)
        {
            DealDamage(data);
        }

        protected override bool _RunEffect(TurnData data, TimeHandler timer)
        {
            return timer.TimeIsUp(delay + duration);
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
            int casterAttack = caster.Stats.GetStat("Attack");
            int targetDefense = caster.Stats.GetStat("Defense");
            int baseDamage = casterAttack - targetDefense/2;

            int totalDamage = (int)(baseDamage * potency / 100);
            totalDamage = Math.Max(totalDamage, 1);
            target.Stats.TakeDamage(totalDamage);
            data.AddDamage(totalDamage);
            DamageNumberManager.ShowDamageNumber(target, totalDamage, DamageType.HealthDamage);
            Utils.Print(this, "Dealt damage: " + totalDamage);
        }
    }
}
