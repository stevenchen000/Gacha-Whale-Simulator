using Godot;
using System;

namespace InventorySystem
{
    [GlobalClass]
    public partial class ItemResource : Resource
    {
        [Export] public Texture2D icon { get; private set; }
        [Export] public int ID { get; private set; }
        [Export] public string itemName { get; private set; }
        [Export(PropertyHint.MultilineText)] public string description { get; private set; }
    }
}