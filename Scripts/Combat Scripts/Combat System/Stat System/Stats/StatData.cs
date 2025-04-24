using System;
using Godot;
using Godot.Collections;

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
        private Dictionary<StatMultiplierType, float> multipliers = new Dictionary<StatMultiplierType, float>();
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
            float result = amount;
            foreach(float multi in multipliers.Values)
            {
                result *= (100 + multi) / 100;
            }

            return (int)result;
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
            return (int)(GetTotalAmount() * (100 + sliderOverflow) / 100);
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

        public void AddMultiplier(StatMultiplierType type, float amountAdded)
        {
            float currTotal = GetTotalAmount();
            AddKeyIfMissing(type);
            multipliers[type] += amountAdded;
            float newTotal = GetTotalAmount();

            if (statType.IsSlidingStat)
            {
                float amountIncrease = newTotal - currTotal;
                slidingAmount += amountIncrease;
            }
        }

        public void RemoveMultiplier(StatMultiplierType type, float amountRemoved)
        {
            AddKeyIfMissing(type);
            multipliers[type] -= amountRemoved;
        }

        private void AddKeyIfMissing(StatMultiplierType type)
        {
            if (!multipliers.ContainsKey(type))
                multipliers[type] = 0;
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

        public void SetSlidingStat(float statAmount)
        {
            int maxAmount = GetSliderMaxAmount();

            slidingAmount = Mathf.Clamp(statAmount, 0, maxAmount);
        }

        public void RemoveAllSlidingStat()
        {
            slidingAmount = 0;
        }

    }
}
