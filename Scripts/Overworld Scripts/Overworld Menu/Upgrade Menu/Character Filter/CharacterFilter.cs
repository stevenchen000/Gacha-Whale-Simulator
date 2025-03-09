using System;
using System.Collections.Generic;
using CombatSystem;
using Godot;

[GlobalClass]
public partial class CharacterFilter : Resource
{
    /// <summary>
    /// Return true to keep character
    /// </summary>
    /// <param name="characters"></param>
    /// <returns></returns>
    public virtual bool Filter(CharacterData character)
    {
        return false;
    }
}
