using System;
using Godot;

namespace DialogueSystem
{
    [GlobalClass]
    public partial class DialogueSceneText : DialogueScene
    {

        public virtual bool RunScene(DialogueManager dm)
        {
            return true;
        }
    }
}
