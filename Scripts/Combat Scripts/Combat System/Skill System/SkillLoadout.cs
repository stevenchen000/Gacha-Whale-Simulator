using Godot;
using Godot.Collections;
using System;

namespace CombatSystem
{
    [GlobalClass]
    public partial class SkillLoadout : Resource
    {
        [Export] public Array<SkillBranch> SkillList;

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