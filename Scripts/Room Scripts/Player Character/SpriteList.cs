using Godot;
using Godot.Collections;
using System;

namespace PlayerCharacterCreator
{
    [GlobalClass]
    public partial class SpriteList : Resource
    {
        [Export] public Array<Texture2D> Textures;
    }
}