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

        public override void _StartElement(BattleSkillCastData data)
        {
            GD.Print("Started dealing damage");
        }

        public override bool _RunElement(BattleSkillCastData data)
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

        public override void _EndElement(BattleSkillCastData data)
        {
            var totalDamage = data.totalDamageDealt;
        }

        private void DealDamageToAllTargets(BattleSkillCastData data, BattleCharacter caster, Array<BattleCharacter> targets)
        {
            foreach(var target in targets)
            {
                DealDamage(data, caster, target);
            }
        }

        private void DealDamage(BattleSkillCastData data, BattleCharacter caster, BattleCharacter target)
        {
            var casterStats = caster.stats;
            var targetStats = target.stats;

            int baseDamage = casterStats.attack - targetStats.defense/2;
            int damage = (int)(baseDamage * totalPotency / numberOfHits / 100);

            targetStats.TakeDamage(damage);
            data.AddDamage(damage);
            DamageNumberManager.ShowDamageNumber(target, damage);
        }
    }
}