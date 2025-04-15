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
        [Export] private AnimationPlayer anim;
        [Export] private Control sizeControl;

        public override void _Ready()
        {
            HideIcon();
            //CallDeferred(MethodName.UpdateDisplay);
        }

        public void SetItem(ItemResource itemToDisplay)
        {
            item = itemToDisplay;
            UpdateDisplay();
        }

        public void SetAmount(int amount)
        {
            if(amountDisplay != null) 
                amountDisplay.Text = amount.ToString();
        }

        public void SetAmount()
        {
            if (amountDisplay != null)
            {
                int amount = GameState.GetAmountOfItemInPlayerInventory(item);
                amountDisplay.Text = amount.ToString();
            }
        }

        public void SetItemIcon(Texture2D texture)
        {
            textureDisplay.Texture = texture;
        }

        public void UpdateDisplay()
        {
            if (item == null) return;

            if (nameDisplay != null) DisplayName();
        }

        private void DisplayTexture()
        {
            textureDisplay.Texture = item.Icon;
        }

        private void DisplayName()
        {
            nameDisplay.Text = item.ItemName;
        }


        /*****************
         * Animations
         * *************/

        public void HideIcon()
        {
            sizeControl.Scale = Vector2.Zero;
        }

        public void PopIn()
        {
            anim.Play("pop_in");
        }

        public void Reset()
        {
            anim.Play("RESET");
        }

    }
}
