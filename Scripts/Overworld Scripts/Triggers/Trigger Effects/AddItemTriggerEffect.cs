using Godot;
using System;
using InventorySystem;

[GlobalClass]
public partial class AddItemTriggerEffect : TriggerEffect
{
    [Export] private ItemResource item;
    [Export] private int baseAmount = 1;
    [Export] private int maxAmount = 5;
    [Export] private bool randomAmount = false;
    
    public override void ActivateEffect(TriggerDetector activator, Trigger trigger)
    {
        int amount = baseAmount;

        if (randomAmount)
        {
            amount = GameState.GetRandomNumber(baseAmount, maxAmount);
        }

        GameState.AddItemToPlayerInventory(item, amount);
    }
}
