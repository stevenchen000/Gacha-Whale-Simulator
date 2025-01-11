using Godot;
using System;

public partial class Test : Control
{

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void TestFunc()
    {
		
	}

	public void GetImageFromSprite(Node node)
    {
		if(node is Sprite2D)
        {
			var sprite = (Sprite2D)node;
			var image = sprite.Texture.GetImage();
			image.SavePng("user://test.png");
			sprite.Texture.ResourcePath = "user://test.png";
			GD.Print($"Texture: {sprite.Texture.ResourcePath}");
			GD.Print($"Image: {image.ResourcePath}");
        }
    }
}
