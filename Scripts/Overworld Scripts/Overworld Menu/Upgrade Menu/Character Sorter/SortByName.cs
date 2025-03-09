using System;
using System.Collections.Generic;
using CombatSystem;
using Godot;
using System.Linq;

[GlobalClass]
public partial class SortByName : CharacterSorter
{
    [Export] private bool descending = false;

    public override void Sort(List<CharacterData> characters)
    {
        Func<CharacterData, string> func = x => x.Character.Name;

        if (descending)
            characters.OrderByDescending(func);
        else
            characters.OrderBy(func);
    }
}

