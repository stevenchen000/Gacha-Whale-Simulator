using Godot;
using Godot.Collections;
using System;

namespace CombatSystem
{
    [Tool]
    [GlobalClass]
    public partial class GetNearestTargets : AITargetSelection
    {
        public override Array<BattleCharacter> GetTargets(BattleManager battle, BattleCharacter caster)
        {
            var enemies = GetEnemies(battle, caster);

            return SortArray(enemies, (x) => DistanceFromTarget(caster, x));
        }

        private int DistanceFromTarget(BattleCharacter caster, BattleCharacter target) 
        {
            var casterPos = caster.currPosition;
            var targetPos = target.currPosition;
            int distance = Math.Abs(casterPos.X - targetPos.X) + 
                           Math.Abs(casterPos.Y - targetPos.Y);

            return distance;
        }
    }
}
