using Godot;
using System;

public partial class Trigger : Area2D
{
	[Export] private PackedScene scene;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void OnBodyEntered(Node2D body)
    {
		if (body.Name == "Player")
		{
			GD.Print("Player entered");
			SceneManager.LoadScene(scene.ResourcePath);
		}
    }
}
