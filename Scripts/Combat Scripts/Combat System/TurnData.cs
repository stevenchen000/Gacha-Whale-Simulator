using System;
using Godot;
using Godot.Collections;

namespace CombatSystem
{
    public class TurnData
    {
        public BattleCharacter caster
        {
            get
            {
                BattleCharacter target = null;
                if (targetData != null) target = targetData.Caster;
                return target;
            }
        }
        public Vector2I casterStartPosition;
        public Vector2I casterPosition; //position where caster was standing at cast time
        public TargetingData targetData;
        public TargetingSelection targetSelection;
        public SkillContainer Skill
        {
            get
            {
                SkillContainer result = null;
                if (targetData != null) result = targetData.Skill;
                return result;
            }
        }
        
        public Array<BattleCharacter> targets;
        public BattleGrid grid { get; private set; }

        public int totalHpDamageDealt { get; private set; } = 0;
        public int totalAmpDamageDealt { get; private set; } = 0;
        public int totalAmountHealed { get; private set; } = 0;
        public int previousHpDamage { get; private set; } = 0;
        public int previousAmpDamage { get; private set; } = 0;



        public TurnData(BattleCharacter caster)
        {
            casterStartPosition = caster.turnStartPosition;
        }

        public TurnData(BattleGrid grid,
                        TargetingData targetData,
                        TargetingSelection selection)
        {
            this.grid = grid;
            this.targetData = targetData;
            targetSelection = selection;
            if (targetData != null) 
                targets = targetData.GetTargets(selection, grid);
        }


        /**********************
         * Damage
         * *******************/

        public void AddHpDamage(int damage)
        {
            totalHpDamageDealt += damage;
            previousHpDamage = damage;
        }

        public void AddAmpDamage(int damage)
        {
            totalAmpDamageDealt += damage;
            previousAmpDamage = damage;
        }

        public void AddHealing(int heal)
        {
            totalAmountHealed += heal;
        }


        /****************
         * Targeting
         * **************/


        
    }
}
