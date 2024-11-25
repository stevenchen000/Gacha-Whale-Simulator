using Godot;
using System;

namespace GachaSystem{
    [GlobalClass]
    public partial class GachaCharacter : Resource
    {
        [Export] public string characterName;
        [Export] public int basePower;
        [Export] public Texture2D characterPortrait;

        [Export] public Rect2 portraitRegion;
        
    }
}