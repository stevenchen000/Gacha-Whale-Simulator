using CombatSystem;
using Godot;
using System;
using InventorySystem;

public partial class LimitBreakMenu : Node
{
    [Export] private CharacterPortraitDisplay portrait;

    [Export] private TextureRect copiesOutline;
    [Export] private Label ownedCopiesLabel;
    [Export] private Label copiesNeededLabel;


    [Export] private TextureRect keysOutline;
    [Export] private Label ownedKeysLabel;
    [Export] private Label keysNeededLabel;
    private CharacterData character;

    public bool useKeys { get; private set; } = false;

    [Signal]
    public delegate void OnUpgradeEventHandler();
    



    public void Init(CharacterData data)
    {
        character = data;
        UpdateDisplay();
        portrait.UpdatePortrait(character.GetPortrait());
    }


    public void SetUseKeys()
    {
        copiesOutline.Modulate = new Color(0,0,0,0);
        keysOutline.Modulate = new Color(1, 1, 1, 1);
        useKeys = true;
    }

    public void SetUseCopies()
    {
        copiesOutline.Modulate = new Color(1,1,1,1);
        keysOutline.Modulate = new Color(0,0,0,0);
        useKeys = false;
    }


    public void LimitBreak()
    {
        if (useKeys)
            LimitBreakWithKeys();
        else
            LimitBreakWithCopies();
        EmitSignal(SignalName.OnUpgrade);
    }



    private void LimitBreakWithCopies()
    {
        character.LimitBreakWithCopies();
    }

    private void LimitBreakWithKeys()
    {
        int keysOwned = GameState.GetAmountOfItemInPlayerInventory(ItemNames.SpiritKeys);
        int keysNeeded = GetKeysNeeded();
        if (keysOwned >= keysNeeded)
        {
            character.LimitBreak();
            GameState.RemoveItemFromPlayerInventory(ItemNames.SpiritKeys, keysNeeded);
        }
    }



    /**************
     * Helpers
     * ************/

    private void UpdateDisplay()
    {
        UpdateCopiesText();
        UpdateKeysText();
    }

    private void UpdateCopiesText()
    {
        int amountOwned = character.ExtraCopies;
        ownedCopiesLabel.Text = amountOwned.ToString();
        
        int amountRequired = GetCopiesNeeded();
        string copiesNeededText = amountRequired <= 0 ? "-" : amountRequired.ToString();
        copiesNeededLabel.Text = copiesNeededText;
    }

    private void UpdateKeysText()
    {
        int amountOwned = GameState.GetAmountOfItemInPlayerInventory(ItemNames.SpiritKeys);
        ownedKeysLabel.Text = amountOwned.ToString();

        int amountRequired = GetKeysNeeded();
        string copiesNeededText = amountRequired <= 0 ? "-" : amountRequired.ToString();
        keysNeededLabel.Text = copiesNeededText;
    }

    private int GetCopiesNeeded()
    {
        return character.CopiesNeededForUpgrade();
    }

    private int GetKeysNeeded()
    {
        return character.KeysNeededForUpgrade();
    }
}
