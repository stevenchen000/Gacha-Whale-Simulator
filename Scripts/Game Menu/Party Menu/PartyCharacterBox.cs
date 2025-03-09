using CombatSystem;
using Godot;
using System;

public partial class PartyCharacterBox : Node
{
    [Export] private CharacterPortraitDisplay display;
    [Export] private CharacterData data;
    [Export] private Label nameBox;
    [Export] private Label copiesBox;

    public void Init(CharacterData newData)
    {
        data = newData;
        display.UpdatePortrait(data.Character.GetPortrait());
        nameBox.Text = newData.Character.Name;
        copiesBox.Text = newData.ExtraCopies.ToString();
    }
}
