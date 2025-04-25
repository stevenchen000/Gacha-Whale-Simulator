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
                bool result = false;
                if (Skill != null) result = Skill.InfiniteUses;
                return result;
            }
        }
        public TargetType TargetType { get { return Skill.targetType; } }


        //Charge
        public bool IsChargeSkill
        {
            get
            {
                bool result = false;
                if (Skill != null) result = Skill.IsChargeSkill;
                return result;
            }
        }
        public int ChargeAmount { get; private set; } = 0;
        public int MaxCharge
        {
            get
            {
                int result = -1;
                if (Skill != null) result = Skill.ChargeAmount;
                return result;
            }
        }
        public float ChargePercent
        {
            get
            {
                return ChargeAmount / (float)MaxCharge * 100;
            }
        }

        public bool IsFullyCharged
        {
            get
            {
                return IsChargeSkill && ChargeAmount >= MaxCharge;
            }
        }

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

        /***************
         * Charging
         * ************/

        public void AddCharge(int amount)
        {
            ChargeAmount += amount;
            ChargeAmount = Math.Min(ChargeAmount, MaxCharge);
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
        
        public Godot.Collections.Array<Vector2I> GetValidStandingPositions(BattleGrid grid, BattleCharacter target)
        {
            var pos = target.currPosition;
            return Skill.GetValidSpaces(grid, pos);
        }
    }
}