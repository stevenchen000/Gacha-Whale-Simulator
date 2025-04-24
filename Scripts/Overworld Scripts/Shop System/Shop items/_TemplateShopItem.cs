using Godot;
using InventorySystem;
using System;

namespace ShopSystem
{
    [GlobalClass]
    public partial class _TemplateShopItem : ShopItemResource
    {
        public override Texture2D GetIcon() { return null; }
        public override int GetAmount() { return 1; }
        public override void BuyItem(GachaAccount account)
        {

        }
    }
}