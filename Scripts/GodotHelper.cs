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
}
