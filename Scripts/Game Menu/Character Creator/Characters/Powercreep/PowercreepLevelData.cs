using System;
using Godot;
using Godot.Collections;

[GlobalClass]
public partial class PowercreepLevelData : Resource
{
    [Export] public Array<PowercreepData> PowercreepData { get; private set; } = new Array<PowercreepData>();
    private bool editable = false;

    public PowercreepLevelData() { }

    
    public void AddPowercreepLevel(CharacterRarity rarity, int powercreepLevel)
    {
        var pc = new PowercreepData(rarity, powercreepLevel);
        PowercreepData.Add(pc);
    }

    public CharacterRarity GetMaxRarity()
    {
        int count = PowercreepData.Count;
        CharacterRarity result = CharacterRarity.N;

        for (int i = count - 1; i >= 0; i--)
        {
            var pc = PowercreepData[i];
            if (pc.IsUnlocked())
            {
                result = pc.Rarity;
                break;
            }
        }
        return result;
    }
}
