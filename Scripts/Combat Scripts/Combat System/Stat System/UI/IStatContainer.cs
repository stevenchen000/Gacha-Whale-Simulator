using Godot;
using System;

namespace CombatSystem
{
    public interface IStatContainer
    {
        public int GetStat(StatType stat);
        public int GetSlidingStat(StatType stat);
    }
}