using Godot;
using System;

public class GodotHelper
{
    public static void MoveNodeToRoot(Node node)
    {
        var rootNode = (Node)node.GetTree().Root;
        var parentNode = node.GetParent();
        parentNode?.RemoveChild(node);
        rootNode.AddChild(node);
    }
}
