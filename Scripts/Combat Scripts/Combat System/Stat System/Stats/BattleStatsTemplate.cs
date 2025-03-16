using System;
using Godot;
using Godot.Collections;

namespace CombatSystem
{
    [GlobalClass]
    public partial class BattleStatsTemplate : Resource
    {
        [Export] public Dictionary<StatType, float> Stats { get; private set; }
        private Dictionary<string, StatType> _statNames;

        public float GetStat(StatType stat)
        {
            float result = stat.BaseAmount;

            if(Stats.ContainsKey(stat))
                result = Stats[stat];

            return result;
        }

        public float GetStat(string statName)
        {
            float result = 0;

            if (_statNames == null)
                InitDict();

            if (_statNames.ContainsKey(statName))
            {
                var stat = _statNames[statName];
                result = Stats[stat];
            }

            return result;
        }
        public Array<StatType> GetStatNames()
        {
            var result = new Array<StatType>();
            result.AddRange(Stats.Keys);
            return result;
        }

        private void InitDict()
        {
            _statNames = new Dictionary<string, StatType>();
            var statKeys = Stats.Keys;

            foreach(var stat in statKeys)
            {
                string name = stat.StatName;
                _statNames[name] = stat;
            }
        }

    }
}
