using Godot;
using Godot.Collections;
using System;


public partial class CharacterDictionary : Node
{
    [Export] private Dictionary<int, GameCharacter> _characters;

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
