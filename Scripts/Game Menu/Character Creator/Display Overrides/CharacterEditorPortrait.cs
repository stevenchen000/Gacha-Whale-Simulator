using Godot;
using System;
using GachaSystem;
using EventSystem;

public partial class CharacterEditorPortrait : Control
{
	[Export] private CharacterPortrait portrait;
	[Export] private CharacterPortraitDisplay portraitUI;
	//UI Elements
	[Export] private Button button;

	[Export] private float scrollDistanceCancel = 5;

	private Vector2 touchPoint;
	private bool hasScrolled = false;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void SetPortrait(CharacterPortrait newPortrait)
    {
		portrait = newPortrait;
		portraitUI.UpdatePortrait(newPortrait);
    }

	public void ButtonAction()
    {
        if (hasScrolled)
        {
			hasScrolled = false;
			//GD.Print("Event canceled, was scrolling");
		}
        else
        {
			
			//GD.Print("Portrait button was pressed");
		}
    }

	private void SelectThisPortrait()
    {

    }

	public void OnButtonInputEvent(InputEvent @event)
    {
		if(@event is InputEventScreenTouch)
        {
			/*var touchEvent = (InputEventScreenTouch)@event;
			int index = touchEvent.Index;
			bool pressed = touchEvent.Pressed;
			if(index == 0)
            {
                if (pressed)
                {
					touchPoint = touchEvent.Position;
                }
                else
                {
					
                }
            }*/
        }else if(@event is InputEventScreenDrag)
        {
			hasScrolled = true;
			/*var dragEvent = (InputEventScreenDrag)@event;var position = dragEvent.Position;
			var distance = (position - touchPoint).Length();

			if(distance > scrollDistanceCancel)
            {
				hasScrolled = true;
            }*/
        }
    }

}
