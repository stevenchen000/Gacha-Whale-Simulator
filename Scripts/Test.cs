using Godot;
using System;

public partial class Test : Control
{
	[Export] private PackedScene scene;

	private bool frameZero = true;
	private int frame = 0;
	private GameMenu menu;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (frame == 0)
        {
            GD.Print(frame);
            menu = (GameMenu)Utils.InstantiateCopy(scene);
            AddChild(menu);
            menu._Init();
		}

		frame++;
	}

	public void TestFunc()
    {
		
	}
}
