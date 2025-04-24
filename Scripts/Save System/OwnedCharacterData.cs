using System;
using System.Runtime.CompilerServices;
using CombatSystem;
using GachaSystem;
using Godot;
using Godot.Collections;

[GlobalClass]
public partial class OwnedCharacterData : Resource
{
    [Export] public Array<CharacterData> OwnedCharacters { get; private set; } = new Array<CharacterData>();
    private Dictionary<int, CharacterData> _characterData;

    public void Init()
    {
        _characterData = new Dictionary<int, CharacterData>();

        if (FileAccess.FileExists(filename))
        {
            Load();
        }

        foreach (var character in OwnedCharacters)
        {
            var charData = character.Character;
            int ID = charData.ID;
            _characterData[ID] = character;
        }
    }


    /*************************
     * Public Functions
     * *******************/


    public void AddCharacterOrDupes(GameCharacter character)
    {
        int ID = character.ID;

        if (IsCharacterOwned(character))
        {
            var charData = _characterData[ID];
            charData.AddCopies(1);
        }
        else
        {
            var charData = CreateCharacterData(character);
            charData.AddCopies(1);
        }
    }

    public bool IsCharacterOwned(GameCharacter character)
    {
        int ID = character.ID;
        return _characterData.ContainsKey(ID);
    }

    public void SetCharacter(CharacterData data)
    {
        OwnedCharacters.Add(data);
        int id = data.Character.ID;
        _characterData[id] = data;
    }

    public CharacterData GetCharacterByID(int id)
    {
        if(_characterData.ContainsKey(id))
            return _characterData[id];
        else
            return null;
    }

    private CharacterData CreateCharacterData(GameCharacter character)
    {
        var charData = new CharacterData(character);
        int ID = character.ID;

        OwnedCharacters.Add(charData);
        _characterData[ID] = charData;

        return charData;
    }




    /****************
     * Save and Load
     * *************/
    
    private string filename = "user://ownedcharacters.json";

    public void Save()
    {
        Utils.Print(this, "Saving data...");
        var data = new Array<string>();

        foreach(var character in OwnedCharacters)
        {
            data.Add(character.ToJson());
        }
        string json = Json.Stringify(data, "\t");

        var file = FileAccess.Open(filename, FileAccess.ModeFlags.Write);
        file.StorePascalString(json);
        file.Close();
    }

    private void Load()
    {
        var file = FileAccess.Open(filename, FileAccess.ModeFlags.Read);
        string json = file.GetPascalString();
        
        var data = (Array<string>)Json.ParseString(json);

        foreach (var item in data)
        {
            var character = new CharacterData(item);
            Utils.Print(this, item);
            SetCharacter(character);
        }

        file.Close();
    }
}