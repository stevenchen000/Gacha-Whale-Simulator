using Godot;
using System;
using Godot.Collections;
using InventorySystem;

namespace GachaSystem{
    [GlobalClass]
    public partial class GachaBanner : Resource
    {
        [Export] public Array<GachaCharacterList> FullGacha { get; private set; }
        [Export] public Array<GachaCharacterList> MultiPullBonus { get; private set; }
        private DateTime expirationDate;
        [Export] public int SinglePullCost { get; private set; } = 300;
        [Export] public int MultiPullCost { get; private set; } = 3000;
        [Export] public ItemResource Currency { get; private set; }


        public Array<GameCharacter> GetMultiPull()
        {
            Array<GameCharacter> result = null;

            if (HasEnoughCurrency(MultiPullCost))
            {
                result = new Array<GameCharacter>();

                for(int i = 0; i < 9; i++)
                {
                    var pulledChar = PullRandomCharacter(FullGacha);
                    result.Add(pulledChar);
                    GameState.AddCharacterToAccount(pulledChar);
                }

                var extraPull = PullRandomCharacter(MultiPullBonus);
                result.Add(extraPull);
                GameState.AddCharacterToAccount(extraPull);

                UseCurrency(MultiPullCost);
            }

            return result;
        }

        public GameCharacter GetSinglePull()
        {
            GameCharacter result = null;

            if (HasEnoughCurrency(SinglePullCost))
            {
                UseCurrency(SinglePullCost);
                result = PullRandomCharacter(FullGacha);
                GameState.AddCharacterToAccount(result);
            }

            return result;
        }


        /*************
         * Cost-Related Functions
         * *************/

        private bool HasEnoughCurrency(int amount)
        {
            return GameState.HasEnoughOfItem(Currency, amount);
        }

        private void UseCurrency(int amount)
        {
            GameState.RemoveItemFromPlayerInventory(Currency, amount);
        }




        /*************
         * Pull-Related Functions
         * ***************/

        private GameCharacter PullRandomCharacter(Array<GachaCharacterList> characterLists)
        {
            GameCharacter result = null;
            var randList = GetRandomList(characterLists);
            result = randList.GetRandomCharacter();

            return result;
        }

        private GachaCharacterList GetRandomList(Array<GachaCharacterList> lists)
        {
            int weight = GetTotalWeight(lists);
            double rand = GameState.GetRandomNumber(0, weight - 1);
            GachaCharacterList result = null;

            for (int i = 0; i < lists.Count; i++)
            {
                var currList = lists[i];
                double chance = currList.Weight;
                rand -= chance;

                if (rand < 0)
                {
                    result = currList;
                    break;
                }
            }

            return result;
        }

        private int GetTotalWeight(Array<GachaCharacterList> characterLists)
        {
            int totalWeight = 0;

            foreach (var list in characterLists)
            {
                totalWeight += list.Weight;
            }

            return totalWeight;
        }

        
    }
}