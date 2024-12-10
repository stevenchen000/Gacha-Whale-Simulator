using Godot;
using System;

public partial class Test : Node2D
{
	[Export] private TimePassedEvent OnTimeUpdate;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		OnTimeUpdate.SubscribeEvent(TestFunc);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void TestFunc(TimeData time)
    {
		if(time.IsTimeInRange(8, 5))
        {
			GD.Print("It's 8:05 AM");
        }
        if (time.IsTimeInRange(1,8,5))
        {
			GD.Print("It's Day 1 at 8:05 AM");
        }
    }
}
