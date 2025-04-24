using Godot;
using System;
using Godot.Collections;

namespace CombatSystem
{
    [Tool]
    [GlobalClass]
    public partial class SkillBranch : Resource
    {
        [Export] private Dictionary<CharacterRarity, CharacterSkill> skillTiers = new Dictionary<CharacterRarity, CharacterSkill>();

        public CharacterSkill GetSkillAtTier(CharacterRarity rarity)
        {
            CharacterSkill result = null;

            if(skillTiers.ContainsKey(rarity))
                result = skillTiers[rarity];

            return result;
        }

    }
}
