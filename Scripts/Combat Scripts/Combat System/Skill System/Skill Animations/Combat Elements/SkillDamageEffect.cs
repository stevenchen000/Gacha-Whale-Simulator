using Godot;
using System;
using Godot.Collections;

namespace CombatSystem
{
    [GlobalClass]
    public partial class SkillDamageEffect : SkillAnimationElement
    {
        [Export] private double totalPotency = 100;
        [Export] private int numberOfHits = 1;
        [Export] private double delayBetweenHits = 0.5;

        public override void _StartElement(TurnData data, TimeHandler time)
        {
            Utils.Print(this, "Started dealing damage");
        }

        public override bool _RunElement(TurnData data, TimeHandler time)
        {
            int prevHitCount = (int)(prevFrame/delayBetweenHits);
            int currHitCount = (int)(currFrame / delayBetweenHits);
            prevHitCount = Math.Max(prevHitCount, 0);
            currHitCount = Math.Min(currHitCount, numberOfHits);

            for(int i = prevHitCount; i < currHitCount; i++)
            {
                var caster = data.caster;
                var targets = data.targets;
                DealDamageToAllTargets(data, caster, targets);
            }
            
            return currHitCount >= numberOfHits;
        }

        public override void _EndElement(TurnData data, TimeHandler time)
        {
            var totalDamage = data.totalHpDamageDealt;
        }

        private void DealDamageToAllTargets(TurnData data, BattleCharacter caster, Array<BattleCharacter> targets)
        {
            foreach(var target in targets)
            {
                DealDamage(data, caster, target);
            }
        }

        private void DealDamage(TurnData data, BattleCharacter caster, BattleCharacter target)
        {
            var casterStats = caster.Stats;
            var targetStats = target.Stats;

            int baseDamage = casterStats.GetStat("Attack") - targetStats.GetStat("Defense")/2;
            int damage = (int)(baseDamage * totalPotency / numberOfHits / 100);

            targetStats.TakeDamage(damage);
            data.AddHpDamage(damage);
            DamageNumberManager.ShowDamageNumber(target, damage, DamageType.HealthDamage);
        }
    }
}