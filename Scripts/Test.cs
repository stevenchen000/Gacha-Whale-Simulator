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
		if(time.IsTimeInRange(13,15))
        {
			GD.Print("Skipped past time 1:15 PM");
        }
        if (time.IsTimeInRange(8, 0))
        {
			GD.Print("Ring ring, time to wake up!");
        }
    }
}
