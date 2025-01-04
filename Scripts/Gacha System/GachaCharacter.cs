using Godot;
using System;

namespace GachaSystem{
    [GlobalClass]
    public partial class GachaCharacter : Resource
    {
        [Export] public string characterName;
        [Export] private string title;
        [Export] public CharacterPortrait portrait;
    }
}