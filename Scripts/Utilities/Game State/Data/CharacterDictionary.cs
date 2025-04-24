using Godot;
using Godot.Collections;
using System;


public partial class CharacterDictionary : Node
{
    private static CharacterDictionary instance;

    [Export] private Array<GameCharacter> _characterList = new();
    private Dictionary<int, GameCharacter> _characters = new();


    public override void _Ready()
    {
        if (instance == null)
        {
            instance = this;
            foreach(var character in _characterList)
            {
                if (character != null)
                {
                    int id = character.ID;
                    _characters[id] = character;
                }
            }
        }
        else
        {
            QueueFree();
        }
    }


    public GameCharacter GetCharacterByID(int id)
    {
        GameCharacter result = null;

        if (_characters != null && _characters.ContainsKey(id))
        {
            result = _characters[id];
        }

        return result;
    }
}
