using Godot;
using System;

namespace CombatSystem
{
    public class SkillContainer
    {
        private CharacterSkill originalSkill;
        public CharacterSkill Skill { get; private set; }
        public int RemainingUses { get; private set; } = 5;
        public bool InfiniteUses
        {
            get
            {
                return Skill.InfiniteUses;
            }
        }
        public TargetType TargetType { get { return Skill.targetType; } }

        public SkillContainer(CharacterSkill skill)
        {
            originalSkill = skill;
            Skill = skill;
            RemainingUses = skill.Uses;
        }



        /******************
         * Skill Overload
         * ****************/

        public void ReplaceSkill(CharacterSkill newSkill)
        {
            Skill = newSkill;
        }

        public void RevertToOriginalSkill()
        {
            Skill = originalSkill;
        }

        /*****************
         * Skill Uses
         * **************/

        public void ConsumeSkillUse(int amount = 1)
        {
            RemainingUses -= amount;
            
            if(RemainingUses < 0) RemainingUses = 0;
        }

        public void AddSkillUse(int amount, bool allowOverflow = false)
        {
            RemainingUses += amount;

            if(RemainingUses > Skill.Uses) RemainingUses = Skill.Uses;
        }

        /*******************
         * Targeting
         * ****************/

        public TargetingData GetTargetingData(BattleGrid grid, BattleCharacter caster, Vector2I position)
        {
            return Skill.GetTargetingData(grid, caster, this, position);
        }

        public bool HasTargetInRange(BattleGrid grid, BattleCharacter caster, Vector2I position)
        {
            var data = Skill.GetTargetingData(grid, caster, this, position);
            return data.ValidTargetExists(grid);
        }
        

    }
}