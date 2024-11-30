using Godot;
using System;

public partial class SceneManager : Node
{
    private static SceneManager instance;
    [Export] private Node rootNode;

    public override void _Ready()
    {
        if (instance == null)
            instance = this;
    }







    public static void LoadScene(string path)
    {
        var scene = ResourceLoader.Load<PackedScene>(path);
        var sceneNode = scene.Instantiate();
        instance.RemoveAllNodes();
        instance.AddNode(sceneNode);
    }

    /// <summary>
    /// Removes all nodes from root except this
    /// </summary>
    private void RemoveAllNodes()
    {
        var children = rootNode.GetChildren();
        foreach(var child in children)
        {
            if(child != this)
            {
                rootNode.RemoveChild(child);
            }
        }
    }

    private void AddNode(Node node)
    {
        rootNode.AddChild(node);
    }


}
