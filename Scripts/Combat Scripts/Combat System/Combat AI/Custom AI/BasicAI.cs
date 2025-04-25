using System;
using System.Collections.Generic;
using Godot;
using Godot.Collections;

namespace CombatSystem
{
    [Tool]
    [GlobalClass]
    public partial class BasicAI : CombatAI
    {

        public override CombatActionData CalculateAction(BattleManager battle, BattleCharacter caster, Array<BattleCharacter> targets)
        {
            CombatActionData result = null;

            var character = battle.GetCurrentCharacter();
            var walkableCoords = character.WalkableSpaces;
            var grid = battle.GetGrid();
            var skills = character.Skills.Skills;

            foreach (var coords in walkableCoords)
            {
                result = CheckActionsAtSpace(battle, grid, character, coords);

                if (result != null) break;
            }

            return result;
        }

        private CombatActionData CheckActionsAtSpace(BattleManager battle, 
                                                     BattleGrid grid,
                                                     BattleCharacter character,
                                                     Vector2I position)
        {
            CombatActionData result = null;
            /*
            var skills = character.Skills.Skills;

            foreach (var skill in skills)
            {
                var baseSkill = skill.Skill;
                var targetingData = baseSkill.GetAllTargetSpaces(battle, grid, character, position);
                var directions = new List<CharacterDirection>();
                directions.AddRange(targetingData.Keys);

                foreach(var direction in directions)
                {
                    var targets = targetingData[direction];
                    RemoveSelfFromTargets(character, targets);

                    if(targets.Count > 0)
                    {
                        result = new CombatActionData(position, skill, direction);
                        Utils.Print(this, $"{position} - {baseSkill.SkillName} - {direction}");
                        break;
                    }
                }

                if (result != null) break;
            }
            */
            return result;
        }

        private void RemoveSelfFromTargets(BattleCharacter character, Array<GridSpace> targetSpaces)
        {
            int index = 0;

            while (index < targetSpaces.Count)
            {
                var space = targetSpaces[index];
                var target = space.CharacterOnSpace;

                if(target == character)
                {
                    targetSpaces.RemoveAt(index);
                    break;
                }
                else
                {
                    index++;
                }
            }
        }
    }
}
