using Godot;
using System;

namespace CombatSystem
{
    [GlobalClass]
    public partial class HealEffect : SkillEffect
    {
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
            int casterAttack = caster.Stats.GetStat("Attack");

            int totalHealing = (int)(casterAttack * potency / 100);
            target.Stats.HealHealth(totalHealing);
            data.AddHealing(totalHealing);
            DamageNumberManager.ShowDamageNumber(target, totalHealing, DamageType.Healing);
            Utils.Print(this, "Healed Health: " + totalHealing);
        }
    }
}
