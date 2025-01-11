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

    public static Node InstantiateCopy(PackedScene scene)
    {
        string path = scene.ResourcePath;
        var newScene = ResourceLoader.Load<PackedScene>(path);
        return scene.Instantiate();
    }

    public static ImageTexture LoadImageFromFile(string path)
    {
        var image = Image.LoadFromFile(path);
        var texture = ImageTexture.CreateFromImage(image);
        return texture;
    }

    public static void Print(Node node, object message)
    {
        GD.Print($"{node.GetType()}: \n\t{message}");
    }

    public static T FindParentOfType<T>(Node node) where T : Node
    {
        T result = null;
        var currNode = node;

        while(result == null)
        {
            var parent = currNode.GetParent();
            if(parent == null)
            {
                break;
            }
            else if(parent is T)
            {
                result = (T)parent;
                break;
            }
            currNode = parent;
        }

        return result;
    }
}
