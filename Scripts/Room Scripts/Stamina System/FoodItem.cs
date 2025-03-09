using System;
using Godot;
using InventorySystem;

[GlobalClass]
public partial class FoodItem : ItemResource
{
    [Export] public int StaminaRestored { get; private set; }
}

