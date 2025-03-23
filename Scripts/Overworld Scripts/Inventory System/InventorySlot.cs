using Godot;
using Godot.Collections;
using System;

namespace InventorySystem
{
    [GlobalClass]
    public partial class InventorySlot : Resource
    {
        [Export] private ItemResource item;
        [Export] private int amount;

        public ItemResource GetItem()
        {
            return item;
        }
        public void SetItem(ItemResource item)
        {
            this.item = item;
        }

        public int GetAmount() { return amount; }
        public void SetAmount(int value) { amount = value; }
        public void AddAmount(int value) { amount += value; }
        public bool HasEnough(int value) { return amount >= value; }
        public void TakeAmount(int value)
        {
            if (HasEnough(value))
            {
                amount -= value;
                if(amount == 0)
                {
                    item = null;
                }
            }
        }

        public bool IsEmpty()
        {
            return amount == 0;
        }

        /**************
         * Save and Load
         * ************/

        public string ToJson()
        {
            string resourcePath = item.ResourcePath;
            int itemAmount = amount;

            var dict = new Dictionary<string, Variant>();
            dict["item"] = resourcePath;
            dict["item_amount"] = itemAmount;

            var json = Json.Stringify(dict);
            
            return json.ToString();
        }

        public void FromJson(string json)
        {
            var dict = (Dictionary<string, Variant>)Json.ParseString(json);
            string itemPath = (string)dict["item"];
            int itemAmount = (int)dict["item_amount"];

            item = ResourceLoader.Load<ItemResource>(itemPath);
            amount = itemAmount;
        }
    }
}