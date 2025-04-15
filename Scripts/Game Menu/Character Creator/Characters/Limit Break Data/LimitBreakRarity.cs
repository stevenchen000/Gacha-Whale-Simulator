using System;
using CombatSystem;
using Godot;
using Godot.Collections;

[GlobalClass]
public partial class LimitBreakRarity : Resource
{
    [Export] public CharacterRarity BaseRarity { get; private set; }
    [Export] private Dictionary<CharacterRarity, LimitBreakDict> dict = new Dictionary<CharacterRarity, LimitBreakDict>();

    public int GetCopiesNeeded(CharacterData character)
    {
        var currRarity = character.Rarity;
        int result = -1;

        Utils.Print(this, currRarity);
        if (dict.ContainsKey(currRarity))
        {
            var currDict = dict[currRarity];
            int currStars = character.Stars;

            result = currDict.GetCopiesNeeded(currStars);
            Utils.Print(this, $"{currStars} - {result}");
        }
        return result;
    }
}

