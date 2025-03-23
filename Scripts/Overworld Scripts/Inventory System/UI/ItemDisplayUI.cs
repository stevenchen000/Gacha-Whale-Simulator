using System;
using Godot;

namespace InventorySystem
{
    public partial class ItemDisplayUI : Node
    {
        [Export] private ItemResource item;
        [Export] private TextureRect textureDisplay;
        [Export] private Label nameDisplay;
        [Export] private Label amountDisplay;

        public override void _Ready()
        {
            CallDeferred(MethodName.UpdateDisplay);
        }

        public void SetItem(ItemResource itemToDisplay)
        {
            item = itemToDisplay;
            UpdateDisplay();
        }

        public void UpdateDisplay()
        {
            if (item == null) return;

            if (textureDisplay != null) DisplayTexture();
            if (nameDisplay != null) DisplayName();
            if(amountDisplay != null) DisplayAmount();
        }

        private void DisplayTexture()
        {
            textureDisplay.Texture = item.Icon;
        }

        private void DisplayName()
        {
            nameDisplay.Text = item.ItemName;
        }

        private void DisplayAmount()
        {
            int amount = GameState.GetAmountOfItemInPlayerInventory(item);
            amountDisplay.Text = amount.ToString();
        }
    }
}
