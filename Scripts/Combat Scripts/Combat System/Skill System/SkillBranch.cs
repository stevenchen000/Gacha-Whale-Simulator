using Godot;
using System;
using Godot.Collections;

namespace CombatSystem
{
    [GlobalClass]
    public partial class SkillBranch : Resource
    {
        [Export] private Array<CharacterSkill> skillTiers = new Array<CharacterSkill>();

        public CharacterSkill GetSkillAtTier(int tier)
        {
            CharacterSkill result = null;

            if (tier >= 0 && tier < skillTiers.Count)
            {
                result = skillTiers[tier];
            }
            else if(tier < 0)
            {
                result = null;
            }
            else
            {
                int index = skillTiers.Count - 1;
                result = skillTiers[index];
            }

            return result;
        }

    }
}
