using Godot;
using System;

namespace CombatSystem
{
    [GlobalClass]
    public partial class DealDamageEffect : SkillEffect
    {
        [Export] private float potency;
        [Export] private float duration = 0.2f;

        protected override void _StartEffect(TurnData data, SkillCastData skillCast)
        {
            DealDamage(data, skillCast);
        }

        protected override bool _RunEffect(TurnData data, SkillCastData skillCast, TimeHandler timer)
        {
            return timer.TimeIsUp(delay + duration);
        }

        protected override void _EndEffect(TurnData data, SkillCastData skillCast)
        {
            
        }

        
        private void DealDamage(TurnData data, SkillCastData skillCast)
        {
            var caster = skillCast.Caster;
            var targets = skillCast.Targets;

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
            data.AddHpDamage(caster, target, totalDamage);
            DamageNumberManager.ShowDamageNumber(target, totalDamage, DamageType.HealthDamage);
            Utils.Print(this, "Dealt damage: " + totalDamage);
        }
    }
}
