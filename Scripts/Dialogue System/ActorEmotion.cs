using Godot;
using System;

namespace DialogueSystem
{
    [GlobalClass]
    public partial class ActorEmotion : Resource
    {
        [Export] public Tag emotion;
        [Export] public Texture2D portrait;
        [Export] public Vector2 size;
        [Export] public Vector2 offset;
        
    }
}