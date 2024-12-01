using System;
using Godot;

namespace DialogueSystem
{
    [GlobalClass]
    public partial class DialogueScene : Resource
    {
        [Export] private string dialogue = "";
        [Export] private Tag emotion;
        [Export] private Actor actor;
        [Export] private Vector2 actorLocation;

        public Texture2D GetPortrait()
        {
            return actor.GetEmotion(emotion).portrait;
        }

        public Vector2 GetPortraitPosition()
        {
            var actorEmotion = actor.GetEmotion(emotion);
            Vector2 offset = actorEmotion.offset;
            return actorLocation + offset;
        }

        public string GetActorName() { return actor.actorName; }
    }
}
