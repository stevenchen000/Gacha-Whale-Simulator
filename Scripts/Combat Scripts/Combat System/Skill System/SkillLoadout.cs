using Godot;
using Godot.Collections;
using System;

namespace CombatSystem
{
    [Tool]
    [GlobalClass]
    public partial class SkillLoadout : Resource
    {
        [Export] public Array<SkillBranch> SkillList;
        [Export(PropertyHint.MultilineText)] private string description = "";
        //0 stars = level cap +10 + amp/hp atk up
        //1 star = Skill 1
        //2 star = Skill 2
        //3 star = Ultimate


        public Array<CharacterSkill> GetSkillsAtLB(CharacterRarity rarity,
                                                   int stars)
        {
            var result = new Array<CharacterSkill>();

            for(int i = 0; i < SkillList.Count; i++)
            {
                var branch = SkillList[i];
                CharacterSkill currSkill = null;
                if(i >= stars - 1) currSkill = GetSkillAtRarity(branch, rarity);
                else currSkill = GetSkillAtPreviousRarity(branch, rarity);

                if(currSkill != null)
                    result.Add(currSkill);
            }

            return result;
        }

        private CharacterSkill GetSkillAtRarity(SkillBranch branch, CharacterRarity rarity)
        {
            return branch.GetSkillAtTier(rarity);
        }

        private CharacterSkill GetSkillAtPreviousRarity(SkillBranch branch, CharacterRarity rarity)
        {
            if (rarity == CharacterRarity.N)
                return null;
            else
                return branch.GetSkillAtTier(rarity - 1);
        }

    }

}