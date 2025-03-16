using System;
using Godot;

namespace CombatSystem
{
    public class StatData
    {
        private StatType statType;
        private float amount = 0;
        private float slidingAmount = 0;
        /// <summary>
        /// 1 = 1%
        /// </summary>
        private float multiplier = 0;
        private float sliderOverflow = 0;


        public StatData(StatType newType, float baseAmount)
        {
            statType = newType;
            amount = baseAmount;
            slidingAmount = baseAmount;
        }


        public int GetBaseAmount()
        {
            return (int)amount;
        }


        public int GetTotalAmount()
        {
            return (int)(amount * (100 + multiplier) / 100);
        }

        public int GetSlidingAmount()
        {
            return (int)slidingAmount;
        }

        /// <summary>
        /// Used for things like HP overflow
        /// </summary>
        /// <returns></returns>
        public int GetSliderMaxAmount()
        {
            return (int)(amount * (100 + multiplier) / 100 * (100 + sliderOverflow) / 100);
        }

        /******************
         * For regular stats
         * ***************/

        public void AddStat(int statAmount)
        {
            amount += statAmount;
        }

        public void RemoveStat(int statAmount)
        {
            float minimum = statType.BaseAmount;
            amount -= statAmount;

            if(amount < minimum) amount = minimum;
        }

        public void AddMultiplier(float amountAdded)
        {
            multiplier += amountAdded;

            if (statType.IsSlidingStat)
            {
                float amountIncrease = amount * amountAdded / 100;
                slidingAmount += amountIncrease;
            }
        }

        public void RemoveMultiplier(float amountRemoved)
        {
            multiplier -= amountRemoved;
        }

        /**************
         * For sliding stats
         * **************/

        public void AddSlidingStat(float statAmount)
        {
            slidingAmount += statAmount;
            int maxAmount = GetSliderMaxAmount();

            if(slidingAmount > maxAmount) slidingAmount = maxAmount;
        }

        public void RemoveSlidingStat(float statAmount)
        {
            slidingAmount -= statAmount;
            
            if(slidingAmount < 0) slidingAmount = 0;
        }


    }
}
