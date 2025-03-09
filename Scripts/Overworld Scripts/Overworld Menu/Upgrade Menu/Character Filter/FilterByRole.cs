using System;
using System.Collections.Generic;
using CombatSystem;
using Godot;

[GlobalClass]
public partial class FilterByRole : CharacterFilter
{
    [Export] private CharacterRole role;

    public override bool Filter(CharacterData character)
    {
        return character.Character.Role == role;
    }
}
