using System;
using Godot;
using Godot.Collections;

namespace DialogueSystem
{
    [GlobalClass]
    public partial class Actor : Resource
    {
        [Export] public string actorName;
        [Export] public Array<ActorEmotion> portraits;


        public ActorEmotion GetEmotion(Tag emotion)
        {
            ActorEmotion result = null;

            foreach (var portrait in portraits)
            {
                if(portrait.emotion == emotion)
                {
                    result = portrait;
                    break;
                }
            }

            return result;
        }
    }
}
