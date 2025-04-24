using Godot;
using InventorySystem;
using System;

namespace ShopSystem
{
    public partial class ShopItem : Resource
    {
        [Export] public ItemResource Item { get; private set; }
        [Export] public int Amount { get; private set; }
        [Export] public ShopCost Cost { get; private set; }


    }
}