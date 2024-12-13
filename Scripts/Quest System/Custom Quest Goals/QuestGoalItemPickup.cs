using Godot;
using System;
using InventorySystem;
using EventSystem;

namespace QuestSystem
{
    [GlobalClass]
    public partial class QuestGoalItemPickup : QuestGoal
    {
        
        [Export] private InventoryChangeEvent OnInventoryChange;
        [Export] private ItemResource itemToCollect;
        [Export] private bool cumulative = false;

        public override void OnQuestStart()
        {
            if (!cumulative)
            {
                
            }
        }

        public override void SetupListeners()
        {
            OnInventoryChange.SubscribeEvent(ListenForEvent);
        }

        public override void RemoveListeners()
        {
            OnInventoryChange.UnsubscribeEvent(ListenForEvent);
        }

        private void ListenForEvent(InventoryChangeData inventoryData)
        {
            if (cumulative)
            {
                int change = inventoryData.amountChanged;
                progress += change;
                //GD.Print($"{currAmount}/{goalAmount}");
            }
            else
            {
                int amountInInventory = inventoryData.newAmount;
                progress = amountInInventory;
            }
        }
    }
}