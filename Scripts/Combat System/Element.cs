using System;
using Godot;

namespace CombatSystem
{
    [GlobalClass]
    public partial class Element : Resource
    {
        [Export] private string name;
        [Export] private Texture2D icon;
    }
}
