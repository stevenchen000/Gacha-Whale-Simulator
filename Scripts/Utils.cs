using Godot;
using System;
using Godot.Collections;

public class Utils
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

    public static T InstantiateCopy<T>(PackedScene scene) where T : Node
    {
        return (T)InstantiateCopy(scene);
    }

    public static ImageTexture LoadImageFromFile(string path)
    {
        var image = Image.LoadFromFile(path);
        var texture = ImageTexture.CreateFromImage(image);
        return texture;
    }

    public static void Print(object node, object message)
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

    /*********************
     * Arrays
     * ****************/
    public delegate bool SortFunc<T>(T t1, T t2);

    /// <summary>
    /// SortFunc should return true when t1 > t2
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="array"></param>
    /// <param name="func"></param>
    public static void SortArray<[MustBeVariant] T>(Array<T> list, SortFunc<T> func)
    {
        GD.Print("Sort Array not implemented!");
    }
}
