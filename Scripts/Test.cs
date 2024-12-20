using Godot;
using System;

public partial class Test : Node2D
{
	[Export] private InfiniNumber a;
	[Export] private InfiniNumber b;
	[Export] private TextureRect imageDisplay;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		/*GD.Print(a.GetNumberAsString());
		GD.Print(b.GetNumberAsString());
		var c = a * b;
		GD.Print(c.GetNumberAsString());
		*/
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void TestFunc(TimeData time)
    {
		
    }
}
