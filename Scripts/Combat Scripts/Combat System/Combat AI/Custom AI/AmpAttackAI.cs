using System;
using Godot;
using Godot.Collections;
using System.Linq;

namespace CombatSystem
{
    [Tool]
    [GlobalClass]
    public partial class AmpAttackAI : CombatAI
    {

        public override CombatActionData CalculateAction(BattleManager battle, BattleCharacter caster, Array<BattleCharacter> targets)
        {
            if (targets.Count == 0) return null;

            var ampAttack = caster.Skills.Skills[0];

            return GetFirstAction(battle, ampAttack, targets);
        }



        private CombatActionData GetFirstAction(BattleManager battle, SkillContainer skill, Array<BattleCharacter> targets)
        {
            CombatActionData result = null;
            var grid = battle.Grid;
            var caster = battle.GetCurrentCharacter();

            foreach (var target in targets)
            {
                var pos = target.currPosition;
                var viablePositions = skill.GetValidStandingPositions(grid, target);
                if(viablePositions.Count > 0)
                {
                    var targetData = skill.GetTargetingData(grid, caster, viablePositions[0]);
                    var selection = targetData.GetValidSelections(grid)[0];
                    result = new CombatActionData(viablePositions[0], skill, targetData, selection, 99999);
                    break;
                }
            }

            return result;
        }

    }
}
