using System;
using Godot;
using Godot.Collections;

namespace DialogueSystem
{
    [GlobalClass]
    public partial class DialogueTree : Resource
    {
        [Export] private Array<DialogueScene> tree;

        public Array<DialogueScene> GetDialogue()
        {
            var result = new Array<DialogueScene>();
            result.AddRange(tree);
            return result;
        }
    }
}
