using System;
using Godot;


[GlobalClass]
public partial class PortraitBorder : Resource
{
    [Export] public Texture2D Background { get; private set; }
    [Export] public Texture2D Border { get; private set; }
}

