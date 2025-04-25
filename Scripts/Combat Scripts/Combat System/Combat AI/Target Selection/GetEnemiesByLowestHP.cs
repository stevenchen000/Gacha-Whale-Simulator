using Godot;
using Godot.Collections;
using System;
using System.Linq;

namespace CombatSystem
{
    [Tool]
    [GlobalClass]
    public partial class GetEnemiesByLowestHP : AITargetSelection
    {
        public override Array<BattleCharacter> GetTargets(BattleManager battle, BattleCharacter caster)
        {
            return GetEnemiesByLowestHealth(battle, caster);
        }

        private Array<BattleCharacter> GetEnemiesByLowestHealth(BattleManager battle, BattleCharacter caster)
        {
            var targets = GetEnemies(battle, caster);
            return SortArray(targets, (x) => x.Stats.GetSlidingStat(StatNames.Health));
        }
    }
}
