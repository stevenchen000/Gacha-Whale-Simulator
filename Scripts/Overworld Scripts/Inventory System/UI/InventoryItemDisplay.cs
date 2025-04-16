using Godot;
using InventorySystem;
using System;

[Tool]
public partial class InventoryItemDisplay : Control
{
    [Export] private ItemResource itemToDisplay;
    [Export] private TextureRect itemIcon;
    [Export] private Label amountLabel;

    private ItemResource prevItem;

    public override void _Ready()
    {
        UpdateDisplay();
    }

    public override void _Process(double delta)
    {
        if (Engine.IsEditorHint())
        {
            if(itemToDisplay != prevItem)
            {
                prevItem = itemToDisplay;
                UpdateDisplay();
            }
        }
    }



    private void UpdateDisplay()
    {
        if (itemToDisplay == null) return;

        if (itemIcon != null)
            UpdateIcon();
        if (amountLabel != null)
            UpdateAmount();
    }

    private void UpdateIcon()
    {
        var icon = itemToDisplay.Icon;
        itemIcon.Texture = icon;
    }

    private void UpdateAmount()
    {
        int amount = GameState.GetAmountOfItemInPlayerInventory(itemToDisplay);
        amountLabel.Text = $"{amount:n0}";
    }
}
