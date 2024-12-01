using Godot;
using System;
using Godot.Collections;
using EventSystem;

public partial class TriggerDetector : Area2D
{
	private Array<Trigger> triggers;
	[Export] private VoidEvent OnSceneLoaded;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		ResetTriggers();
		//OnSceneLoaded?.SubscribeEvent(ResetTriggers);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (Input.IsActionJustPressed("ui_accept"))
		{
			if(triggers.Count > 0)
			{
				triggers[0].ActivateTrigger();
			}
		}
	}

	public void OnTriggerEnter(Area2D area)
	{
		if(area is Trigger)
		{
			Trigger trigger = (Trigger)area;
			if (trigger.ActivatesOnEnter)
			{
				trigger.ActivateTrigger();
			}
			else
			{
				triggers.Add(trigger);
			}
			GD.Print("Entered trigger");
		}
	}

	public void OnTriggerExit(Area2D area)
	{
        if (area is Trigger)
        {
            Trigger trigger = (Trigger)area;
            triggers.Remove(trigger);
            GD.Print("Exited trigger");
        }
    }

	private void ResetTriggers()
	{
		triggers = new Array<Trigger>();
	}
}
