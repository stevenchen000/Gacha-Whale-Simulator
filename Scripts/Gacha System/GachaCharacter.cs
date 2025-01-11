using Godot;
using System;

namespace GachaSystem{
    [GlobalClass]
    public partial class GachaCharacter : Resource
    {
        [Export] public string characterName;
        [Export] public string title; //used for variant
        [Export] public CharacterPortrait portrait;
    }
}