using System;
using System.Collections.Generic;
using CombatSystem;
using Godot;
using System.Linq;

[GlobalClass]
public partial class SortByLevel : CharacterSorter
{
    [Export] private bool descending = false;

    public override void Sort(List<CharacterData> characters)
    {
        Func<CharacterData, int> func = x => x.Level;

        if (descending)
            characters.OrderByDescending(func);
        else
            characters.OrderBy(func);
    }
}

