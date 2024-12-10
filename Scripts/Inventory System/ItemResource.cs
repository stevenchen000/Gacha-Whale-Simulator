using Godot;
using System;

namespace InventorySystem
{
    [GlobalClass]
    public partial class ItemResource : Resource
    {
        [Export] private Texture2D icon;
        [Export] private string itemName;
    }
}