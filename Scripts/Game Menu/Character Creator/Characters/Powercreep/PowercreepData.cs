using System;
using Godot;


[Tool]
[GlobalClass]
public partial class PowercreepData : Resource
{
    [Export] public CharacterRarity Rarity { get; private set; }
    [Export] public int PowercreepLevel { get; private set; }
    /// <summary>
    /// The ID of the event that unlocks this. 
    /// Only for pre-made characters, custom
    /// characters will add powercreep levels as they unlock
    /// </summary>
    [Export] private int unlockEventID = -1;

    public PowercreepData() { }

    public PowercreepData(CharacterRarity rarity, int powercreepLevel)
    {
        Rarity = rarity;
        PowercreepLevel = powercreepLevel;
    }

    public bool IsUnlocked()
    {
        Utils.Print(this, "Don't forget to add unlock conditions");
        return unlockEventID == -1;
    }
}

