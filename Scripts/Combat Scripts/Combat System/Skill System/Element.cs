using System;
using Godot;

namespace CombatSystem
{
    [GlobalClass]
    public partial class Element : Resource
    {
        [Export] private string Name;
        [Export] public Texture2D Texture { get; private set; }
    }
}
