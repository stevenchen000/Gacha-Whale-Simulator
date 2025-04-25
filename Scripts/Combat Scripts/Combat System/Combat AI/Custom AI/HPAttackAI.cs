using System;
using System.Collections.Generic;
using Godot;
using Godot.Collections;

namespace CombatSystem
{
    [Tool]
    [GlobalClass]
    public partial class HPAttackAI : CombatAI
    {
        public override CombatActionData CalculateAction(BattleManager battle, BattleCharacter caster, Array<BattleCharacter> targets)
        {
            if(targets.Count == 0) return null;
            var hpAtk = caster.Skills.Skills[1];
            var grid = battle.Grid;

            var spaces = GetAllViableSpaces(grid, targets, hpAtk);
            var targetSpace = spaces[0];
            var targetData = hpAtk.GetTargetingData(grid, caster, targetSpace);
            var selections = targetData.GetValidSelections(grid);
            var selection = selections[0];

            return new CombatActionData(targetSpace, hpAtk, targetData, selection, 99999);
        }

        private Array<Vector2I> GetAllViableSpaces(BattleGrid grid, Array<BattleCharacter> targets, SkillContainer skill)
        {
            var result = new Array<Vector2I>();

            foreach(var target in targets)
            {
                var validSpaces = GetViableSpacesFromTarget(grid, target, skill);
                foreach(var space in validSpaces)
                {
                    if(!result.Contains(space))
                        result.Add(space);
                }
            }

            return result;
        }

        private Array<Vector2I> GetViableSpacesFromTarget(BattleGrid grid, BattleCharacter target, SkillContainer skill)
        {
            return skill.GetValidStandingPositions(grid, target);
        }
    }
}
