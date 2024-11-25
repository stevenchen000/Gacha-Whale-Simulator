using Godot;
using System;
using Godot.Collections;

namespace GachaSystem{
    [GlobalClass]
    public partial class GachaCharacterList : Resource
    {
        [Export] public Array<GachaCharacter> allSSRCharacters {get; set;}
        [Export] public Array<GachaCharacter> allSRCharacters {get; set;}
        [Export] public Array<GachaCharacter> allRCharacters {get; set;}
    }
}