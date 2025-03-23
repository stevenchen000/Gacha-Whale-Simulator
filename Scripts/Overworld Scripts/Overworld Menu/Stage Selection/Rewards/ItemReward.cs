using System;
using Godot;
using InventorySystem;

[GlobalClass]
public partial class ItemReward : StageReward
{
    [Export] private ItemResource item;
    [Export] private int amount = 1;

    public override void ReceiveReward() 
    {
        GameState.AddItemToPlayerInventory(item, amount);
    }

    public override Texture2D GetTexture() 
    { 
        return item.Icon;
    }

    public override int GetAmount()
    {
        return amount;
    }
}
