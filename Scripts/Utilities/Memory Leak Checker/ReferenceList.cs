using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class ReferenceList
{
    public string NodeName { get; private set; }

    private List<WeakReference> refs = new List<WeakReference>();
    private List<string> nodeNames = new List<string>();

    public ReferenceList(Node node)
    {
        NodeName = node.Name;
        AddChildren(node);
    }

    public int GetNumberOfLivingNodes()
    {
        UpdateStatus();
        return refs.Count;
    }

    public void PrintLivingNodes()
    {
        UpdateStatus();
        string namesList = "The following nodes are still alive:";
        foreach (var name in nodeNames)
        {
            namesList += ", " + name;
        }
        Utils.Print(this, namesList);
    }

    private void UpdateStatus()
    {
        int index = 0;

        while (index < refs.Count)
        {
            var currRef = refs[index];
            if (!currRef.IsAlive)
            {
                refs.RemoveAt(index);
                nodeNames.RemoveAt(index);
            }
            else
            {
                index++;
            }
        }
    }

    private void AddChildren(Node child)
    {
        var currRef = new WeakReference(child);
        refs.Add(currRef);
        nodeNames.Add(child.Name);

        foreach (var grandchild in child.GetChildren())
        {
            AddChildren(grandchild);
        }
    }
}

