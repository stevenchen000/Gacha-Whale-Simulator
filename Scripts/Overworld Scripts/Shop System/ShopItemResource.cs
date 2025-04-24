using Godot;
using InventorySystem;
using System;

namespace ShopSystem
{
    [GlobalClass]
    public partial class ShopItemResource : Resource
    {
        public virtual Texture2D GetIcon() { return null; }
        public virtual int GetAmount() { return 1; }

        public virtual void BuyItem(GachaAccount account)
        {

        }
    }
}