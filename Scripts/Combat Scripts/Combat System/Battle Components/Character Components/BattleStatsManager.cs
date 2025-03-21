using Godot;
using System.Collections.Generic;
using System;
using System.Security.Cryptography;

namespace CombatSystem
{
    public partial class BattleStatsManager : Node, IStatContainer
    {
        private Dictionary<StatType, StatData> _stats = new Dictionary<StatType, StatData>();
        private Dictionary<string, StatType> _statNames = new Dictionary<string, StatType>();

        [Signal] public delegate void StatChangedEventHandler();

        /*****************
         * Init
         * **************/

        public void _Init(CharacterData character)
        {
            InitRoleStats(character);
            InitNamesDict();
        }

        private void InitRoleStats(CharacterData character)
        {
            var role = character.Character.Role;
            var growth = role.stats;
            int level = character.Level;

            var statNames = growth.GetStatNames();

            foreach (var statName in statNames)
            {
                float amount = growth.GetStatAmount(statName, level);
                //For any features that add to base stats, put calculation here
                var statData = new StatData(statName, amount);
                _stats[statName] = statData;
            }
        }

        private void InitNamesDict()
        {
            var statNames = new List<StatType>();
            statNames.AddRange(_stats.Keys);

            foreach(var stat in statNames)
            {
                _statNames[stat.StatName] = stat;
            }
        }

        /***************
         * Amp Damage
         * *************/

        public int TakeAmpDamage(int damage, bool canBreak = true)
        {
            int ampAmount = GetSlidingStat(StatNames.Amp);

            RemoveSlidingStat(StatNames.Amp, damage);
            
            return ampAmount;
        }

        public void AddAmpAmount(int amount)
        {
            AddSlidingStat(StatNames.Amp, amount);
        }

        public void ConsumeAmp(float percent = 100)
        {
            int currAmp = (int) (GetSlidingStat(StatNames.Amp) * percent / 100);
            RemoveSlidingStat(StatNames.Amp, currAmp);
        }


        /****************
         * Health
         * *************/

        public void TakeDamage(int damage)
        {
            RemoveSlidingStat("Health", damage);
        }

        public void HealHealth(int healing)
        {
            AddSlidingStat("Health", healing);
        }

        public bool IsDead()
        {
            float currHealth = GetSlidingStat("Health");
            return currHealth <= 0;
        }

        /***************
         * Stat Getters
         * *************/

        public int GetStat(StatType type)
        {
            int result = type.BaseAmount;

            if (_stats.ContainsKey(type))
            {
                var stat = _stats[type];
                result = stat.GetTotalAmount();
            }

            return result;
        }

        public int GetStat(string name)
        {
            var type = GetStatTypeFromString(name);
            return GetStat(type);
        }

        public void AddStatMultiplier(StatType type, float amount)
        {
            if (_stats.ContainsKey(type))
            {
                var stat = _stats[type];
                stat.AddMultiplier(amount);

                RaiseEvent();
            }
        }

        /***************
         * Sliding Stats
         * ****************/

        public int GetSlidingStat(StatType type)
        {
            int result = type.BaseAmount;

            if (type.IsSlidingStat && _stats.ContainsKey(type))
            {
                var stat = _stats[type];
                result = stat.GetSlidingAmount();
            }

            return result;
        }

        public int GetSlidingStat(string name)
        {
            var stat = GetStatTypeFromString(name);
            return GetSlidingStat(stat);
        }

        public void AddSlidingStat(StatType type, int amount)
        {
            var stat = _stats[type];
            stat.AddSlidingStat(amount);

            RaiseEvent();
        }

        public void AddSlidingStat(string name, int amount)
        {
            var stat = GetStatTypeFromString(name);
            AddSlidingStat(stat, amount);
        }

        public void RemoveSlidingStat(StatType type, int amount)
        {
            var stat = _stats[type];
            stat.RemoveSlidingStat(amount);

            RaiseEvent();
        }

        public void RemoveSlidingStat(string name, int amount)
        {
            var stat = GetStatTypeFromString(name);
            RemoveSlidingStat(stat, amount);
        }

        public void SetSlidingStat(StatType type, int value)
        {
            var stat = _stats[type];
            stat.SetSlidingStat(value);

            RaiseEvent();
        }


        /*****************
         * Helpers
         * **************/

        private StatType GetStatTypeFromString(string name)
        {
            return _statNames[name];
        }

        private void RaiseEvent()
        {
            EmitSignal(SignalName.StatChanged);
        }

        /*****************
         * Debug
         * ***************/

        public void PrintStats()
        {
            foreach(var statName in _stats.Keys)
            {
                int amount = GetStat(statName);
                int slidingAmount = GetSlidingStat(statName);

                if (statName.IsSlidingStat)
                {
                    Utils.Print(this, $"{statName.StatName}: {slidingAmount}/{amount}");
                }
                else
                {
                    Utils.Print(this, $"{statName.StatName}: {amount}");
                }
            }
        }
    }
}