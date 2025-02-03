using Godot;
using System;
using Godot.Collections;
using EventSystem;

public partial class TriggerDetector : Area2D
{
	private Player player;
	private Array<Trigger> triggers;
	[Export] private VoidEvent OnSceneLoaded;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		ResetTriggers();
		player = (Player)GetParent();
		//OnSceneLoaded?.SubscribeEvent(ResetTriggers);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (Input.IsActionJustPressed("ui_accept") && !player.IsMovementLocked())
		{
			if(triggers.Count > 0)
			{
				triggers[0].ActivateTrigger(this);
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
				trigger.ActivateTrigger(this);
			}
			else
			{
				triggers.Add(trigger);
			}
            Utils.Print(this, "Entered trigger");
		}
	}

	public void OnTriggerExit(Area2D area)
	{
        if (area is Trigger)
        {
            Trigger trigger = (Trigger)area;
            triggers.Remove(trigger);
            Utils.Print(this, "Exited trigger");
        }
    }

	private void ResetTriggers()
	{
		triggers = new Array<Trigger>();
	}
}
