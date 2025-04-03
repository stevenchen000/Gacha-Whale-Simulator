using Godot;
using System;
using System.Collections.Generic;

namespace CombatSystem
{
    public partial class SkillManager : Node
    {
        public List<SkillContainer> Skills { get; private set; } = new List<SkillContainer>();
        private SimpleWeakRef<BattleCharacter> _caster;
        private BattleCharacter caster
        {
            get
            {
                return _caster.Value;
            }
            set
            {
                _caster = new SimpleWeakRef<BattleCharacter> (value);
            }
        }



        public void Init(CharacterData data)
        {
            var charSkills = data.GetSkills();

            foreach (var skill in charSkills)
            {
                var skillContainer = new SkillContainer(skill);
                Skills.Add(skillContainer);
            }
        }

        public void ReplaceSkill(int index, CharacterSkill skill)
        {
            var container = GetSkillAtIndex(index);
            container?.ReplaceSkill(skill);
        }

        public void RevertToOriginal(int index)
        {
            var container = GetSkillAtIndex(index);
            container?.RevertToOriginalSkill();
        }


        /**************
         * Helpers
         * ************/

        private SkillContainer GetSkillAtIndex(int index)
        {
            SkillContainer result = null;

            if (index < 0)
            {
                result = Skills[0];
            }
            else if (index < Skills.Count)
            {
                result = Skills[index];
            }

            return result;
        }
    }
}