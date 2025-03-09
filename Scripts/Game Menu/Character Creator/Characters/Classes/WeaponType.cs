using Godot;
using System;
using EventSystem;
using CombatSystem;

[GlobalClass]
public partial class WeaponType : Resource
{
	[Export] public string Name { get; private set; }
	[Export] public Texture2D Texture { get; private set; }
	[Export(PropertyHint.MultilineText)] public string Description { get; private set; }
}
