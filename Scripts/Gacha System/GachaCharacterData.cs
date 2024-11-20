using Godot;
using System;

namespace GachaSystem{
    [GlobalClass]
    public partial class GachaCharacterData : Resource
    {
        [Export] public GachaCharacter baseCharacter;
        [Export] public int currentLevel;
        [Export] public int numberOfCopies;
    }
}