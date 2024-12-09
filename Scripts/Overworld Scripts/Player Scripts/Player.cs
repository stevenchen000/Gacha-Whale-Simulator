using Godot;
using System;
using Godot.Collections;
using EventSystem;

public partial class Player : CharacterBody2D
{
	public const float Speed = 300.0f;
	public const float JumpVelocity = -400.0f;
	private Array<Trigger> currentTriggers;

	private int movementLock = 0;
	[Export] private VoidEvent OnPlayerMovementLocked;
	[Export] private VoidEvent OnPlayerMovementUnlocked;
	

    public override void _Ready()
    {
        base._Ready();

    }

    public override void _PhysicsProcess(double delta)
	{
		if(movementLock <= 0)
        {
			MoveCharacter();
        }
        else
        {
			StopCharacter();
        }
	}

	public void LockMovement()
    {
		movementLock++;
		if(movementLock == 1)
        {
			OnPlayerMovementLocked.RaiseEvent();
        }
		GD.Print($"Locked: {movementLock} locks in place");
    }

	public void UnlockMovement()
    {
		movementLock--;
		if(movementLock < 0)
        {
			GD.Print("WARNING: There is a negative number of locks on character movement");
        }else if(movementLock == 0)
        {
			OnPlayerMovementUnlocked.RaiseEvent();
		}
		GD.Print($"Unlocked: {movementLock} locks in place");
	}

	/******************
	 * Helpers
	 * **************/

	private void MoveCharacter()
    {
		Vector2 velocity = Velocity;

		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		Vector2 direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
		direction = direction.Normalized();

		Velocity = direction * Speed;
		MoveAndSlide();
	}

	private void StopCharacter()
    {
		Velocity /= 2;
		MoveAndSlide();
    }
}
