using Godot;
using System;
using GachaSystem;

namespace CharacterCreator
{
	public partial class EditorPortraitDisplay : Button
	{
		//UI Elements
		[Export] private CharacterPortraitDisplay display;

		private CustomCharacterPortrait portrait;
		[Export] private PortraitBorder portraitBorder;
		private CustomGameCharacter character;
		[Export] private PortraitBorder characterBorder;

		private SelectionMenu ui;

		private bool dragged = false;
		private Vector2 touchPoint;
		[Export] private float dragDistanceBeforeCancel = 10;

        /**************************
		 * Main Functions
		 * *********************/

        #region Main Functions

        // Called when the node enters the scene tree for the first time.
        public override void _Ready()
		{
			ui = Utils.FindParentOfType<SelectionMenu>(this);
		}

		// Called every frame. 'delta' is the elapsed time since the previous frame.
		public override void _Process(double delta)
		{
		}

        public override void _GuiInput(InputEvent @event)
        {
            if(@event is InputEventScreenDrag)
            {
				var dragEvent = (InputEventScreenDrag)@event;
				int index = dragEvent.Index;
				if (index == 0 && !dragged)
				{
					var touchPos = dragEvent.Position;
					float distance = (touchPos - touchPoint).Length();
					//GodotHelper.Print(this, distance);
					if(distance > dragDistanceBeforeCancel)
						dragged = true;
				}
            }

			if(@event is InputEventScreenTouch)
            {
				var touchEvent = (InputEventScreenTouch)@event;
				int index = touchEvent.Index;
				if (index == 0 && !touchEvent.Pressed)
                {
					if (dragged)
					{
						dragged = false;
						//GodotHelper.Print(this, "Nothing happened");
					}
					else
					{
						UpdateUI();
						//GodotHelper.Print(this, "Updated UI");
					}
                }else if(index == 0 && touchEvent.Pressed)
				{
					touchPoint = touchEvent.Position;
				}
            }
        }

		#endregion

		private void UpdateUI()
		{
			//Utils.Print(this, "Updating UI");
			if (portrait != null)
				ui.SetCreatorPortrait(portrait);
			else if (character != null)
				ui.SetCreatorCharacter(character);
		}


		public void SetPortrait(CustomCharacterPortrait portrait)
		{
			character = null;
			this.portrait = portrait;
			display.UpdatePortrait(portrait);
			display.SetBorder(portraitBorder);
		}

		public void SetCharacter(CustomGameCharacter character)
        {
			portrait = null;
			this.character = character;
			var characterPortrait = character.GetPortrait();
			display.UpdatePortrait(characterPortrait);
			display.SetBorder(characterBorder);
        }


	}
}