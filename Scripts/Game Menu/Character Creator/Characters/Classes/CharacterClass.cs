using Godot;
using System;
using EventSystem;

[GlobalClass]
public partial class CharacterClass : Resource
{
	[Export] public string Name { get; private set; }
	[Export] public Texture2D Texture { get; private set; }
	[Export] public CharacterRole Role { get; private set; }
	[Export] public WeaponType Weapon { get; private set; }
    [Export(PropertyHint.MultilineText)] public string Description { get; private set; }
	
}
