using System;
using Godot;
using Godot.Collections;

namespace DialogueSystem
{
    [GlobalClass]
    public partial class DialogueTree : Resource
    {
        [Export] private Array<DialogueScene> tree;

        public DialogueScene GetDialogueAtIndex(int index)
        {
            if (index < tree.Count)
                return tree[index];
            else
                return null;
        }

        public DialogueScene GetNextScene(DialogueScene scene)
        {
            DialogueScene result = null;

            for(int i = 0; i < tree.Count; i++)
            {
                DialogueScene currScene = tree[i];
                if(scene == currScene && i+1 < tree.Count)
                {
                    result = tree[i + 1];
                    break;
                }
            }

            return result;
        }

        public DialogueScene GetDialogueStart()
        {
            return tree[0];
        }

    }
}
