using System;
using Godot;
using Godot.Collections;

namespace CombatSystem
{
    [GlobalClass]
    public partial class AmpDamageFormula : BaseDamageFormula
    {
        [Export] private StatType casterAttackStat;
        [Export] private StatType targetDefenseStat;

        public override int CalculateDamage(BattleCharacter caster, BattleCharacter target, double potency)
        {
            var casterStats = caster.Stats;
            var targetStats = target.Stats;
            int atk = casterStats.GetStat(casterAttackStat);
            int def = targetStats.GetStat(targetDefenseStat);

            double weaknessMultiplier = GetElementMultiplier(caster, target);
            int baseDamage = atk - def / 2;
            int totalDamage = (int)(baseDamage * potency / 100 * weaknessMultiplier);

            totalDamage = Math.Max(totalDamage, 1);

            return totalDamage;
        }

        private double GetElementMultiplier(BattleCharacter caster, BattleCharacter target)
        {
            var casterElement = caster.CharacterElement;
            var targetElement = target.CharacterElement;

            var targetWeakness = ElementChart.GetWeakness(targetElement);
            var targetResistance = ElementChart.GetResistance(targetElement);

            double result = 1;

            if(casterElement == targetWeakness)
            {
                result = 1.5;
            }
            else if(casterElement == targetResistance)
            {
                result = 0.5;
            }
            else
            {
                result = 1;
            }

            return result;
        }
    }
}
