using CombatSystem;
using Godot;
using System;

public partial class TurnOrderPortrait : Node
{
    [Export] private CharacterPortraitDisplay display;
    [Export] private PortraitBorder playerBorder;
    [Export] private PortraitBorder enemyBorder;

    private CharacterData data;


    public void UpdatePortrait(BattleCharacter character)
    {
        bool isPlayer = character.IsPlayer();
        data = character.Character;
        display.UpdatePortrait(data.GetPortrait());

        if(isPlayer) display.SetBorder(playerBorder);
        else display.SetBorder(enemyBorder);
    }
}
