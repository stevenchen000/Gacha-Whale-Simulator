using Godot;
using Godot.Collections;
using System;

namespace CombatSystem
{
    [GlobalClass]
    public partial class SkillLoadout : Resource
    {
        [Export] public Array<SkillBranch> SkillList;
        [Export(PropertyHint.MultilineText)] private string description = "";
        //0 stars = level cap +10 + amp/hp atk up
        //1 star = Skill 1
        //3 star = Skill 2
        //5 star = Ultimate


        public Array<CharacterSkill> GetSkillsAtLB(CharacterRarity baseRarity,
                                                   CharacterRarity rarity,
                                                   int stars)
        {
            var result = new Array<CharacterSkill>();
            
            int rarityDiff = (int)rarity - (int)baseRarity;

            for (int i = 0; i < SkillList.Count; i++)
            {
                CharacterSkill skill = null;

                if (IsCurrTier(stars, i))
                {
                    skill = _GetSkillAtLB(rarityDiff, i);
                }
                else
                {
                    skill = _GetSkillAtLB(rarityDiff - 1, i);
                }

                if (skill != null) result.Add(skill);
            }

            return result;
        }

        //Checks whether the skill should use the current tier
        //   or previous tier
        private bool IsCurrTier(int stars, int index)
        {
            bool result = false;

            if(stars == 0 && index == 0)
            {
                result = false;
            }
            else
            {
                result = stars / 2 > index;
            }

            return result;
        }

        private CharacterSkill _GetSkillAtLB(int rarityDifference, int index)
        {
            var branch = SkillList[index];
            return branch.GetSkillAtTier(rarityDifference);
        }

        public Array<CharacterSkill> GetSkillsAtLevel(int level)
        {
            var result = new Array<CharacterSkill>();

            foreach (var branch in SkillList)
            {
                var skill = branch.GetSkillAtTier(level);
                result.Add(skill);
            }

            return result;
        }
    }

}