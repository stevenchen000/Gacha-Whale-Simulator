using Godot;
using System;
using EventSystem;
using Godot.Collections;

public partial class SceneManager : Node
{
	private static SceneManager instance;
	private Node rootNode;
	[Export] private VoidEvent OnSceneLoad; //called when loading the scene
	[Export] private VoidEvent OnFadeOutFinished; //used for checking if load screen has finished fading out
	[Export] private VoidEvent OnFadeInFinished;
	[Export] private Array<string> nodeWhitelist;

	[Export] private LoadScreen loadScreen;

	private string resourcePath;
	private bool loadScene = false;
	private bool loadingStarted = true;

	private bool frameZero = true;

	public override void _Ready()
	{
		if (instance == null)
		{
			instance = this;
			rootNode = FindRootNode();
			//rootNode.AddChild(this);
			loadScreen.Init();
			OnFadeOutFinished?.SubscribeEvent(() => loadScene = true);
			OnFadeInFinished?.SubscribeEvent(() => loadingStarted = false);
		}
		else
		{
			
		}
	}

	public override void _Process(double delta)
	{
        if (frameZero)
        {
			Reparent(rootNode);
			//GetParent().RemoveChild(this);
			frameZero = false;
		}

		if (loadScene)
		{
			loadScene = false;
			LoadSceneInProcess();
		}
	}

	private void LoadSceneInProcess()
	{
		var scene = ResourceLoader.Load<PackedScene>(resourcePath);
		var sceneNode = scene.Instantiate();
		instance.RemoveAllNodes();
		instance.AddNode(sceneNode);
		OnSceneLoad?.RaiseEvent(this);
	}

	public static void LoadScene(string path)
	{
		if (!instance.loadingStarted)
		{
			instance.resourcePath = path;
			//instance.loadScreen.Activate();
		}
	}

	/// <summary>
	/// Removes all nodes from root except this
	/// </summary>
	private void RemoveAllNodes()
	{
		var children = rootNode.GetChildren();
		foreach(var child in children)
		{
			if(child != this && !nodeWhitelist.Contains(child.Name))
			{
				rootNode.RemoveChild(child);
			}
		}
	}

	private void AddNode(Node node)
	{
		rootNode.AddChild(node);
	}

	private Node FindRootNode()
	{
		Node result = null;

		result = GetTree().Root;

		return result;
	}
}
