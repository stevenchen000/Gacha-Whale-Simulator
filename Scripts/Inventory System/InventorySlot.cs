using Godot;
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
    }
}