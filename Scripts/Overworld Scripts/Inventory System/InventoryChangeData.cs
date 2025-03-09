using System;

namespace InventorySystem
{
    public class InventoryChangeData
    {
        public bool addedItem { get; private set; }
        public ItemResource item { get; private set; }
        public int amountChanged { get; private set; }
        public int newAmount { get; private set; }

        public InventoryChangeData(ItemResource item, int amountChanged, int newAmount, bool addedItem = true)
        {
            this.addedItem = addedItem;
            this.item = item;
            this.amountChanged = amountChanged;
            this.newAmount = newAmount;
        }
    }
}
