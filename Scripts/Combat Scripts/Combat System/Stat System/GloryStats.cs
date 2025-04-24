using System;
using Godot;

namespace CombatSystem
{
    public class GloryStats
    {
        //Used for stat multipliers in tower climbing

        public int OffenseLevel { get; private set; } = 1;
        public int DefenseLevel { get; private set; } = 1;
        public int SupportLevel { get; private set; } = 1;

        public int BonusPerLevel { get; private set; } = 10;
        public int BaseCost { get; private set; } = 10;
        public double CostMultiplier { get; private set; } = 1.1;


        public GloryStats() { }

        public GloryStats(int offense, int defense, int support)
        {
            OffenseLevel = offense;
            DefenseLevel = defense;
            SupportLevel = support;
        }

        public GloryStats(int lvl)
        {
            OffenseLevel = lvl;
            DefenseLevel = lvl;
            SupportLevel = lvl;
        }

        /**************
         * Getters
         * ************/

        public void LevelUpOffense()
        {
            OffenseLevel++;
        }

        



        /*****************
         * Helpers
         * **************/
        private int _GetCost(int currLevel)
        {
            double multiplier = Mathf.Pow(CostMultiplier, currLevel-1);
            return (int)(BaseCost * multiplier);
        }

    }
}
