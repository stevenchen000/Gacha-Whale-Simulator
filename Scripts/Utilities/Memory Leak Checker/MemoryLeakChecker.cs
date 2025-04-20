using Godot;
using System;
using System.Collections.Generic;

public partial class MemoryLeakChecker : Node
{
    private static MemoryLeakChecker instance;

    private List<ReferenceList> refs = new List<ReferenceList>();

    public override void _Ready()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            QueueFree();
        }
    }


    public static void TrackAllChildren(Node node)
    {
        var list = new ReferenceList(node);

        instance.refs.Add(list);
    }

    public static void CheckStatus()
    {
        instance.UpdateStatus();
    }

    private void UpdateStatus()
    {
        int index = 0;

        while (index < refs.Count)
        {
            var reference = refs[index];
            if (reference.GetNumberOfLivingNodes() > 0)
            {
                reference.PrintLivingNodes();
                index++;
            }
            else
            {
                refs.RemoveAt(index);
            }
        }
    }



}
