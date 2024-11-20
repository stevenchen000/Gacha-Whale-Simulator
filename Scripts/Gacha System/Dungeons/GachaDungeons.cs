using Godot;
using System;

namespace GachaSystem.Dungeons{
    public partial class GachaDungeons : Resource
    {
        [Export] private Dungeon mainStory;
        [Export] private Dungeon[] sideDungeons;
        
    }
}