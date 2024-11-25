using Godot;
using System;
using GachaSystem;

[GlobalClass]
public partial class GachaValuePair : Resource
{
    [Export] public GachaCharacter character;
    [Export] public int power;

    public GachaValuePair() { }
    public GachaValuePair(GachaCharacter character, int power)
    {
        this.character = character;
        this.power = power;
    }
}
