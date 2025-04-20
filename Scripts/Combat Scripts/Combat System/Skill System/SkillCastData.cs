using Godot.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CombatSystem
{
    public class SkillCastData
    {
        public BattleCharacter Caster { get; private set; }
        public Array<BattleCharacter> Targets { get; private set; } = new Array<BattleCharacter>();
        public SkillContainer Skill { get; private set; }


        public TargetingData Targeting { get; private set; }
        public TargetingSelection Selection {  get; private set; }


        public SkillCastData(BattleCharacter caster, 
                             BattleGrid grid, 
                             SkillContainer skill, 
                             TargetingData targeting, 
                             TargetingSelection selection)
        {
            Caster = caster;
            Targets = targeting.GetTargets(selection, grid);
            Skill = skill;
            Targeting = targeting;
            Selection = selection;
        }





        /// <summary>
        /// Used for effects like poisons where a skill tied to an effect is used
        /// </summary>
        /// <param name="caster"></param>
        /// <param name="targets"></param>
        /// <param name="skill"></param>
        public SkillCastData(BattleCharacter caster, Array<BattleCharacter> targets, CharacterSkill skill)
        {
            Skill = new SkillContainer(skill);
            Caster = caster;
            Targets.AddRange(targets);
        }

        /// <summary>
        /// Used for effects like counters where the character's skill is used, but
        /// no targeting data is used
        /// </summary>
        /// <param name="caster"></param>
        /// <param name="targets"></param>
        /// <param name="skill"></param>
        public SkillCastData(BattleCharacter caster, Array<BattleCharacter> targets, SkillContainer skill)
        {
            Skill = skill;
            Caster = caster;
            Targets.AddRange(targets);
        }
    }
}
