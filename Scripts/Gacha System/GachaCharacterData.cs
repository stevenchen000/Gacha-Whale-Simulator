using Godot;
using System;

namespace GachaSystem{
    [GlobalClass]
    public partial class GachaCharacterData : Resource
    {
        [Export] public GachaCharacter baseCharacter { get; set; }
        [Export] public int currentLevel { get; set; }
        [Export] public int numberOfCopies { get; set; }

        public GachaCharacterData(GachaCharacter character)
        {
            baseCharacter = character;
            currentLevel = 1;
            numberOfCopies = 1;
        }

        
    }
}