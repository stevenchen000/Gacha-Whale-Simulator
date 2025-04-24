using Godot;
using Godot.Collections;
using InventorySystem;
using System;

namespace ShopSystem
{
    [GlobalClass]
    public partial class ShopCost : Resource
    {
        [Export] public Dictionary<ItemResource, int> Costs { get; private set; } = new Dictionary<ItemResource, int>();

        public Array<ItemResource> GetItemsNeeded()
        {
            var result = new Array<ItemResource>();
            result.AddRange(Costs.Keys);
            return result;
        }

        public int GetAmountNeeded(ItemResource item)
        {
            if (Costs.ContainsKey(item))
                return Costs[item];
            else
                return 0;
        }

        public bool HasEnoughItems(Inventory inventory)
        {
            bool result = true;
            foreach(var item in GetItemsNeeded())
            {
                int amountNeeded = GetAmountNeeded(item);
                if(!inventory.HasEnough(item, amountNeeded))
                {
                    result = false;
                    break;
                }
            }

            return result;
        }
    }
}