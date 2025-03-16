using System;
using CombatSystem;
using Godot;
using Godot.Collections;


[GlobalClass]
public partial class PartySetup : Resource
{
    [Export] private string name = "";
    [Export] public int PartyID { get; private set; }
    [Export] public Array<CharacterData> Party = new Array<CharacterData>();

    private static int partySize = 5;

    public PartySetup()
    {

    }

    public void CopyParty(PartySetup party)
    {
        Party = party.Party;
    }


    public void SetParty(Array<CharacterData> party)
    {
        Party.Clear();
        Party.AddRange(party);

        while(Party.Count < 5)
        {
            Party.Add(null);
        }
    }

    private int GetNumberOfPartyMembers()
    {
        int result = 0;

        foreach(var member in Party)
        {
            if (member != null)
                result++;
        }

        return result;
    }

    private void CreateEmptyParty()
    {
        Party = new Array<CharacterData>();

        for (int i = 0; i < partySize; i++)
        {
            Party.Add(null);
        }
    }

    public bool HasMember(CharacterData data)
    {
        return Party.Contains(data);
    }

    public void SetMember(int index, CharacterData data)
    {
        Party[index] = data;
        Utils.Print(this, "Remember to check for duplicate party members");
    }

    public string GetPartyName()
    {
        string result = "";

        if(name == "")
        {
            result = $"Party {PartyID+1}";
        }
        else
        {
            result = name;
        }

        return result;
    }

    public void SetPartyName(string newName)
    {
        name = newName;
    }


    /*********************
     * Save/Load
     * ********************/

    private string GetFlagName()
    {
        return $"party_{PartyID}_data";
    }


    public void Save()
    {
        var data = new Array<int>();

        for (int i = 0; i < partySize; i++)
        {
            var member = Party[i];

            if(member != null)
            {
                int id = member.Character.ID;
                data.Add(id);
            }
            else
            {
                data.Add(-1);
            }
        }

        string json = Json.Stringify(data);
        GameState.SetStringFlag(GetFlagName(), json);
    }

    public void Load()
    {
        CreateEmptyParty();
        TryToLoadData();

        if(GetNumberOfPartyMembers() == 0)
        {
            LoadDefaultParty();
        }
    }

    private void LoadDefaultParty()
    {
        var character = GameState.GetOwnedCharacterByID(1);
        
        Party[0] = character;
    }

    private void TryToLoadData()
    {
        string json = GameState.GetStringFlag(GetFlagName());

        if (json != "") 
        { 
            var data = (Array<int>)Json.ParseString(json);
            var characters = new Array<CharacterData>();

            for (int i = 0; i < partySize; i++)
            {
                int id = data[i];

                if (id != -1)
                {
                    var character = GameState.GetOwnedCharacterByID(id);
                    characters.Add(character);
                }
                else
                {
                    characters.Add(null);
                }
            }

            SetParty(characters);
        }
    }
}

