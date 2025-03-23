using Godot;
using System;

namespace InventorySystem
{
    [GlobalClass]
    public partial class ItemResource : Resource
    {
        [Export] public Texture2D Icon { get; private set; }
        [Export] public int ID { get; private set; }
        [Export] public string ItemName { get; private set; }
        [Export] public ItemType type { get; private set; }
        [Export(PropertyHint.MultilineText)] public string Description { get; private set; }
    }
}