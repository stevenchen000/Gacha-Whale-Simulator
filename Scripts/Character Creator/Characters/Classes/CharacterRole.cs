using Godot;
using System;
using EventSystem;

[GlobalClass]
public partial class CharacterRole : Resource
{
	[Export] public string Name { get; private set; }
	[Export] public Texture2D Texture { get; private set; }
}
