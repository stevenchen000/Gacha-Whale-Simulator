using Godot;
using InventorySystem;
using System;

namespace ShopSystem
{
    [GlobalClass]
    public partial class BasicShopItem : ShopItemResource
    {
        [Export] public ItemResource Item { get; private set; }
        [Export] public int Amount { get; private set; }
        public override Texture2D GetIcon() 
        { 
            return Item.Icon; 
        }
        
        public override int GetAmount() 
        { 
            return Amount;
        }
        public override void BuyItem(GachaAccount account)
        {

        }
    }
}