using Godot;
using System;
using Godot.Collections;
using EventSystem;

namespace InventorySystem
{
    public partial class Inventory : Node, ISavable
    {
        [Export] private Array<InventorySlot> slots = new Array<InventorySlot>();
        [Export] private VoidEvent OnInventoryChange;

        private Dictionary<ItemResource, InventorySlot> _slotsDict;

        public override void _Ready()
        {
            if (slots == null)
                slots = new Array<InventorySlot>();
            InitDictionary();
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

        public bool HasEnough(ItemResource item, int amount)
        {
            var slot = FindSlotWithItem(item);
            int totalAmount = 0;

            if(slot != null)
            {
                totalAmount = slot.GetAmount();
            }

            return totalAmount >= amount;
        }

        public void DeleteItem(ItemResource item)
        {
            int amount = GetItemCount(item);
            RemoveItem(item, amount);
        }

        public Array<ItemResource> GetItemsOfType(ItemType type)
        {
            var result = new Array<ItemResource>();
            foreach(var slot in slots)
            {
                var item = slot.GetItem();
                if(item.type == type)
                {
                    result.Add(item);
                }
            }

            return result;
        }

        /********************
         * Get Items Of type
         * *****************/

        public Array<T> GetItemsOfType<[MustBeVariant]T>() where T : ItemResource
        {
            var result = new Array<T>();

            foreach(var slot in slots)
            {
                var item = slot.GetItem();

                if (item is T)
                {
                    var itemT = (T)item;
                    result.Add(itemT);
                }
            }
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

            _slotsDict[item] = newSlot;
        }

        private InventorySlot FindSlotWithItem(ItemResource item)
        {
            InventorySlot result = null;

            int id = item.ID;
            if (_slotsDict.ContainsKey(item))
            {
                result = _slotsDict[item];
            }

            return result;
        }

        private void RaiseEvent(ItemResource item, int newAmount, int change, bool addedItem)
        {
            var eventData = new InventoryChangeData(item, change, newAmount, addedItem);
            //OnInventoryChange.RaiseEvent(this, eventData);
            OnInventoryChange?.RaiseEvent(this);
        }

        private void RaiseEvent()
        {
            OnInventoryChange?.RaiseEvent(this);
        }


        /******************
         * Init
         * *************/

        private void InitDictionary()
        {
            _slotsDict = new Dictionary<ItemResource, InventorySlot>();

            for(int i = 0; i < slots.Count; i++)
            {
                var slot = slots[i];
                var item = slot.GetItem();
                int id = item.ID;

                _slotsDict[item] = slot;
            }
        }




        /*********************
         * Save and Load
         * *****************/

        private string GetFileName()
        {
            return $"{Name}.json";
        }

        public void Save()
        {
            var result = new Array<string>();

            foreach(var slot in slots)
            {
                string slotJson = slot.ToJson();
                result.Add(slotJson);
            }

            string filename = GetFileName();
            string json = Json.Stringify(result);

            var file = FileAccess.Open(filename, FileAccess.ModeFlags.Write);
            file.StorePascalString(json);
            file.Close();
        }

        public void Load()
        {
            string filename = GetFileName();
            Utils.Print(this, "Loading inventory...");
            if (FileAccess.FileExists(filename))
            {
                var file = FileAccess.Open(filename, FileAccess.ModeFlags.Read);
                string json = file.GetPascalString();
                file.Close();

                var list = (Array<string>)Json.ParseString(json);
                foreach(var itemJson in list)
                {
                    _AddItemFromJson(itemJson);
                }
            }
        }

        private void _AddItemFromJson(string itemJson)
        {
            var itemDict = (Dictionary<string, Variant>)Json.ParseString(itemJson);
            string itemFilename = (string)itemDict["item"];
            int amount = (int)itemDict["item_amount"];
            var item = ResourceLoader.Load<ItemResource>(itemFilename);
            AddItem(item, amount);
            Utils.Print(this, $"Loaded an item! {item.ItemName}: {amount}");
        }
    }
}