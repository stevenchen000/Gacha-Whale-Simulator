using System;
using Godot;

namespace DialogueSystem
{
    [GlobalClass]
    public partial class DialogueScene : Resource
    {
        [Export] public string dialogue = "";
        [Export] public Tag emotion;
        [Export] public Actor actor;
        [Export] public Vector2 actorLocation;

        public virtual bool RunScene(DialogueManager dm)
        {
            return true;
        }

        public Texture2D GetPortrait()
        {
            return actor.GetEmotion(emotion).portrait;
        }

        public Vector2 GetPortraitPosition()
        {
            var actorEmotion = actor.GetEmotion(emotion);
            return actorEmotion.offset;
        }

        public Vector2 GetPortraitSize()
        {
            var actorEmotion = actor.GetEmotion(emotion);
            return actorEmotion.size;
        }

        public string GetActorName() { return actor.actorName; }
    }
}
