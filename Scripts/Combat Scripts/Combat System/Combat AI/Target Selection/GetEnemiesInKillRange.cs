using Godot;
using Godot.Collections;
using System;
using System.Linq;

namespace CombatSystem
{
    [Tool]
    [GlobalClass]
    public partial class GetEnemiesInKillRange : AITargetSelection
    {
        public override Array<BattleCharacter> GetTargets(BattleManager battle, BattleCharacter caster)
        {
            var enemies = GetEnemies(battle, caster);
            var result = GetValidTargets(caster, enemies);
            return SortEnemiesByHighestHealth(result);
        }

        private Array<BattleCharacter> SortEnemiesByHighestHealth(Array<BattleCharacter> targets)
        {
            return SortArray(targets, (x) => x.Stats.GetSlidingStat(StatNames.Health), true);
        }

        private Array<BattleCharacter> GetValidTargets(BattleCharacter caster, Array<BattleCharacter> targets)
        {
            Array<BattleCharacter> results = new();

            foreach (var target in targets)
            {
                if (CharacterIsKillable(caster, target))
                {
                    results.Add(target);
                }
            }

            return results;
        }

        private bool CharacterIsKillable(BattleCharacter caster, BattleCharacter target)
        {
            var casterAmp = caster.Stats.GetSlidingStat(StatNames.Amp);
            var enemyHealth = target.Stats.GetSlidingStat(StatNames.Health);

            return enemyHealth <= casterAmp;
        }
    }
}
