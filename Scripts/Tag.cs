using Godot;
using System;

[GlobalClass]
public partial class Tag : Resource
{
    [Export] public string tag { get; set; }
    [Export] public Texture2D icon { get; set; }
}
