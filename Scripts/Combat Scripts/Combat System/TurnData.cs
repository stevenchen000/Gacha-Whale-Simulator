using System;
using System.Collections.Generic;
using Godot;
using Godot.Collections;

namespace CombatSystem
{
    public class TurnData
    {
        public BattleCharacter Caster { get; private set; }
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
        
        public List<BattleCharacter> Targets { get; private set; } = new List<BattleCharacter>();

        public SkillCastData MainSkillCast { get; private set; }
        public List<SkillCastData> SkillCasts { get; private set; } = new List<SkillCastData>();
        private List<SkillCastData> completedSkills = new List<SkillCastData>();
        public BattleManager Battle { get; private set; }
        public BattleGrid Grid { get; private set; }

        public int totalHpDamageDealt { get; private set; } = 0;
        public int totalAmpDamageDealt { get; private set; } = 0;
        public int totalAmountHealed { get; private set; } = 0;
        public int previousHpDamage { get; private set; } = 0;
        public int previousAmpDamage { get; private set; } = 0;


        private System.Collections.Generic.Dictionary<BattleCharacter, int> enemyDamageTaken = new System.Collections.Generic.Dictionary<BattleCharacter, int>();
        private System.Collections.Generic.Dictionary<BattleCharacter, int> attackerDamageDealt = new System.Collections.Generic.Dictionary<BattleCharacter, int>();




        public TurnData(BattleCharacter caster)
        {
            Caster = caster;
            casterStartPosition = caster.turnStartPosition;
        }

        public TurnData(BattleManager battle,
                        TargetingData targetData,
                        TargetingSelection selection)
        {
            Battle = battle;
            Grid = battle.Grid;
            var caster = targetData.Caster;
            var skill = targetData.Skill;
            var newSkillCast = new SkillCastData(caster, Grid, skill, targetData, selection);
            MainSkillCast = newSkillCast;
        }


        public void AddSkillCast(BattleCharacter caster, Array<BattleCharacter> targets, CharacterSkill skill)
        {
            var newSkillCast = new SkillCastData(caster, targets, skill);
            SkillCasts.Add(newSkillCast);
        }

        public SkillCastData GetCurrentSkillCast() 
        {
            if (SkillCasts.Count > 0)
                return SkillCasts[0];
            else
                return null;
        }

        public void RemoveCurrentSkill()
        {
            if(SkillCasts.Count > 0) 
            {
                var cast = SkillCasts[0];
                completedSkills.Add(cast);
                SkillCasts.RemoveAt(0);
            }
        }

        public bool HasSkillQueued()
        {
            //Utils.Print(this, SkillCasts.Count);
            return SkillCasts.Count > 0;
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

        
        private void AddMissingEntry(System.Collections.Generic.Dictionary<BattleCharacter, int> dictionary, BattleCharacter character)
        {
            if (!dictionary.ContainsKey(character))
                dictionary[character] = 0;
        }
    }
}
