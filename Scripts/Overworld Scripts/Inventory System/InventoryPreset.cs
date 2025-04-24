using System;
using Godot;
using Godot.Collections;

namespace InventorySystem
{
    [GlobalClass]
    public partial class InventoryPreset : Resource
    {
        [Export] public string Name { get; private set; } = "";
        public Dictionary<ItemResource, int> Items { get; private set; } = new Dictionary<ItemResource, int>();


    }
}
