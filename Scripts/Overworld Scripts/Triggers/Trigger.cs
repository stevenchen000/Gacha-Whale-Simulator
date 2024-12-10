using Godot;
using System;
using Godot.Collections;

public partial class Trigger : Area2D
{
	[Export] public bool ActivatesOnEnter { get; set; }
	[Export] private Array<TriggerEffect> effects;
	private Node triggerObject;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		triggerObject = (Node)GetParent();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void ActivateTrigger(TriggerDetector activator)
    {
        foreach(var effect in effects)
		{
			effect.ActivateEffect(activator, this);
		}
    }
}
