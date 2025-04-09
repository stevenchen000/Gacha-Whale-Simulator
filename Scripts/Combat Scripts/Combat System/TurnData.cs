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
        public BattleManager Battle { get; private set; }
        public BattleGrid Grid { get; private set; }

        public int totalHpDamageDealt { get; private set; } = 0;
        public int totalAmpDamageDealt { get; private set; } = 0;
        public int totalAmountHealed { get; private set; } = 0;
        public int previousHpDamage { get; private set; } = 0;
        public int previousAmpDamage { get; private set; } = 0;


        private Dictionary<BattleCharacter, int> enemyDamageTaken = new Dictionary<BattleCharacter, int>();
        private Dictionary<BattleCharacter, int> attackerDamageDealt = new Dictionary<BattleCharacter, int>();




        public TurnData(BattleCharacter caster)
        {
            casterStartPosition = caster.turnStartPosition;
        }

        public TurnData(BattleManager battle,
                        TargetingData targetData,
                        TargetingSelection selection)
        {
            Battle = battle;
            Grid = battle.Grid;
            this.targetData = targetData;
            targetSelection = selection;
            if (targetData != null) 
                targets = targetData.GetTargets(selection, Grid);
        }


        /**********************
         * Damage
         * *******************/

        public void AddHpDamage(BattleCharacter attacker, BattleCharacter target, int damage)
        {
            totalHpDamageDealt += damage;
            previousHpDamage = damage;
            AddDamageDealt(attacker, damage);
            AddDamageTaken(target, damage);
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

        public int GetDamageTaken(BattleCharacter target)
        {
            if(enemyDamageTaken.ContainsKey(target))
                return enemyDamageTaken[target];
            else
                return 0;
        }

        public int GetDamageDealt(BattleCharacter attacker)
        {
            if(attackerDamageDealt.ContainsKey(attacker))
                return attackerDamageDealt[attacker];
            else
                return 0;
        }

        /****************
         * Helpers
         * **************/

        private void AddDamageTaken(BattleCharacter target, int damage)
        {
            AddMissingEntry(enemyDamageTaken, target);
            enemyDamageTaken[target] += damage;
        }

        private void AddDamageDealt(BattleCharacter attacker, int damage)
        {
            AddMissingEntry(attackerDamageDealt, attacker);
            attackerDamageDealt[attacker] += damage;
        }

        
        private void AddMissingEntry(Dictionary<BattleCharacter, int> dictionary, BattleCharacter character)
        {
            if (!dictionary.ContainsKey(character))
                dictionary[character] = 0;
        }
    }
}
