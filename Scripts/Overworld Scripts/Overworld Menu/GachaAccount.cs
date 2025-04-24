using System;
using CombatSystem;
using Godot;
using Godot.Collections;
using InventorySystem;

[GlobalClass]
public partial class GachaAccount : Resource
{
    [ExportCategory("Character Data")]
    [Export] public string Username { get; private set; }
    [Export] public Array<CharacterData> OwnedCharacters { get; private set; } = new Array<CharacterData>();
    [Export] public Array<PartySetup> Parties { get; private set; } = new Array<PartySetup>();
    [Export] public InventoryPreset PlayerInventory { get; private set; }
    public GameFlags Flags { get; private set; } = new GameFlags();
    public GameStringFlags StringFlags { get; private set; } = new GameStringFlags();
    public GloryStats GloryStats { get; private set; } = new GloryStats();


    public GachaAccount()
    {
        var defaultCharacter = new CharacterData(GameState.GetCharacterByID(1));
        OwnedCharacters.Add(defaultCharacter);
        for(int i = 0; i < 10; i++)
        {
            Parties.Add(new PartySetup(defaultCharacter));
        }
    }



    public string ToJson()
    {
        Dictionary<string, string> dict = new Dictionary<string, string>();
        string result = "";

        dict["Username"] = Username;

        return result;
    }

    public void FromJson(string json)
    {

    }
}
