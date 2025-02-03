using Godot;
using System;
using GachaSystem;

public partial class ScrollableButton : Control
{
	[Export] private GachaCharacter character;
	[Export] private CharacterPortrait portrait;
	//UI Elements
	[Export] private Button background;
	[Export] private Sprite2D portraitElement;
	[Export] private Label nameElement;

	[Export] private float scrollDistanceCancel = 5;

	private Vector2 touchPoint;
	private bool hasScrolled = false;

	//Portrait variables
	[Export] private GachaCharacterData characterData;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void UpdatePortrait(Texture2D texture)
    {
		nameElement.Text = "";
		portraitElement.Texture = texture;
    }

	public void UpdatePortrait(GachaCharacter character)
	{
		if(character == null){
			portraitElement.Texture = null;
		}else{
			portrait.SetupPortrait(background, portraitElement);
			nameElement.Text = character.characterName;
		}

		this.character = character;
	}

	public void UpdatePortrait(CharacterPortrait portrait)
    {
		if(portrait == null)
        {
			portraitElement.Texture = null;
        }
        else
        {
			portrait.SetupPortrait(background, portraitElement, 5);
        }
    }

	public void Test()
    {
        if (hasScrolled)
        {
			hasScrolled = false;
            Utils.Print(this, "Event canceled, was scrolling");
		}
        else
        {
            Utils.Print(this, "Portrait button was pressed");
		}
    }

	public void OnButtonInputEvent(InputEvent @event)
    {
		if(@event is InputEventScreenTouch)
        {
			var touchEvent = (InputEventScreenTouch)@event;
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
            }
        }
		if(@event is InputEventScreenDrag)
        {
			var dragEvent = (InputEventScreenDrag)@event;
			var position = dragEvent.Position;
			var distance = (position - touchPoint).Length();

			if(distance > scrollDistanceCancel)
            {
				hasScrolled = true;
            }
        }
    }

}
