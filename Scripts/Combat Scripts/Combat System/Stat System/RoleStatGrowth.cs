using System;
using Godot;
using Godot.Collections;

namespace CombatSystem
{
    [Tool]
    [GlobalClass]
    public partial class RoleStatGrowth : Resource
    {
        //Stats at level 1
        [Export] private BattleStatsTemplate _baseStats;
        [Export] private BattleStatsTemplate _statsPerLevel;

        public int GetStatAmount(StatType stat, int level)
        {
            float baseStat = _baseStats.GetStat(stat);
            float extraStats = _statsPerLevel.GetStat(stat) * level;

            return (int)(baseStat + extraStats);
        }

        public Array<StatType> GetStatNames()
        {
            return _baseStats.GetStatNames();
        }
    }
}
