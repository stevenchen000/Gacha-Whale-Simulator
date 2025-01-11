using Godot;
using System;
using Godot.Collections;
using EventSystem;

namespace InventorySystem
{
    public partial class Inventory : Node
    {
        [Export] private Array<InventorySlot> slots;
        [Export] private InventoryChangeEvent OnInventoryChange;

        public override void _Ready()
        {
            if (slots == null)
                slots = new Array<InventorySlot>();
        }

        public override void _Process(double delta)
        {
            
        }

        public void AddItem(ItemResource item, int amount = 1)
        {
            if (item == null) return;
            if (amount <= 0) return;

            var slot = FindSlotWithItem(item);
            if(slot != null)
            {
                slot.AddAmount(amount);
                RaiseEvent(item, slot.GetAmount(), amount, true);
            }
            else
            {
                AddNewSlot(item, amount);
                RaiseEvent(item, amount, amount, true);
            }
        }

        /// <summary>
        /// Tries to remove item
        /// If there is not enough of item, 
        /// fails and returns false
        /// </summary>
        /// <param name="item"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        public bool RemoveItem(ItemResource item, int amount = 1)
        {
            if (item == null) return false;
            if (amount <= 0) return false;

            var slot = FindSlotWithItem(item);
            bool hasEnough = false;
            if (slot != null)
            {
                hasEnough = slot.HasEnough(amount);

                if(hasEnough)
                    slot.TakeAmount(amount);
                if (slot.IsEmpty())
                    slots.Remove(slot);
                RaiseEvent(item, slot.GetAmount(), amount, false);
            }

            return hasEnough;
        }

        public int GetItemCount(ItemResource item)
        {
            int result = 0;
            var slot = FindSlotWithItem(item);
            if (slot != null)
                result = slot.GetAmount();

            return result;
        }

        /***************
         * Helpers
         * **************/
        private void AddNewSlot(ItemResource item, int amount)
        {
            var newSlot = new InventorySlot();
            newSlot.SetItem(item);
            newSlot.SetAmount(amount);
            slots.Add(newSlot);
        }

        private InventorySlot FindSlotWithItem(ItemResource item)
        {
            InventorySlot result = null;

            foreach(var slot in slots)
            {
                if(slot.GetItem() == item)
                {
                    result = slot;
                    break;
                }
            }

            return result;
        }

        private void RaiseEvent(ItemResource item, int newAmount, int change, bool addedItem)
        {
            var eventData = new InventoryChangeData(item, change, newAmount, addedItem);
            OnInventoryChange.RaiseEvent(this, eventData);
            
        }

    }
}