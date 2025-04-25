using Godot;
using Godot.Collections;
using System;
using System.Linq;

namespace CombatSystem
{
    [Tool]
    [GlobalClass]
    public partial class GetEnemiesByHighestAmp : AITargetSelection
    {
        public override Array<BattleCharacter> GetTargets(BattleManager battle, BattleCharacter caster)
        {
            var enemies = GetEnemies(battle, caster);

            return SortByAmp(enemies);
        }

        private Array<BattleCharacter> SortByAmp(Array<BattleCharacter> enemies)
        {
            return SortArray(enemies, (x) => x.Stats.GetSlidingStat(StatNames.Amp), true);
        }
    }
}
