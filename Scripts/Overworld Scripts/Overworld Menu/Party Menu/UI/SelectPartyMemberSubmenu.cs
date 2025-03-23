using Godot;
using Godot.Collections;
using System;

namespace PartyMenuSystem
{
    public partial class SelectPartyMemberSubmenu : Control
    {
        private SimpleWeakRef<PartyMenu> menu;
        [Export] private Control characterContainer;
        [Export] private PackedScene buttonScene;
        [Export] private PartyCharacterDisplay partyDisplayPreview;
        private Array<PartyMemberSelectionButton> buttons;


        public override void _Ready()
        {
            base._Ready();
            Disable();
            CallDeferred(MethodName._Init);
        }

        public void _Init()
        {
            var tempMenu = Utils.FindParentOfType<PartyMenu>(this);
            menu = new SimpleWeakRef<PartyMenu>(tempMenu);

            FindInitialButtons();
            SetupButtons();
        }

        public void Disable()
        {
            Visible = false;
            Position = new Vector2(9999, 9999);
        }

        public void Enable()
        {
            Visible = true;
            Position = new Vector2(0, 0);
            SetupButtons();
            UpdateDisplay();
        }

        private void UpdateDisplay()
        {
            var character = menu.Value.GetCurrentCharacter();
            partyDisplayPreview.SetupDisplay(character);
        }


        /******************
         * Button Helpers
         * ****************/

        private void FindInitialButtons()
        {
            buttons = new Array<PartyMemberSelectionButton>();

            var children = characterContainer.GetChildren();

            for(int i = 0; i < children.Count; i++)
            {
                var child = children[i];

                if (child is PartyMemberSelectionButton)
                {
                    buttons.Add(child as PartyMemberSelectionButton);
                }
            }
        }

        private void SetupButtons()
        {
            SetupEnoughButtons();
            InitAllButtons();
        }

        private void InitAllButtons()
        {
            var characters = GameState.GetAllOwnedCharacters();

            for(int i = 0; i < characters.Count; i++)
            {
                var character = characters[i];
                var button = buttons[i];
                button.SetupButton(character);
            }
        }

        private void SetupEnoughButtons()
        {
            var characters = GameState.GetAllOwnedCharacters();
            int numOfChars = characters.Count;
            int numOfButtons = buttons.Count;

            int difference = numOfChars - numOfButtons;

            if(difference > 0)
            {
                for(int i = 0; i < difference; i++)
                {
                    var button = Utils.InstantiateCopy<PartyMemberSelectionButton>(buttonScene);
                    buttons.Add(button);
                    characterContainer.AddChild(button);
                }
            }
            else
            {
                for(int i = 0; i < -difference; i++)
                {
                    int lastIndex = buttons.Count - 1;
                    var button = buttons[lastIndex];
                    button.QueueFree();
                    buttons.RemoveAt(lastIndex);
                }
            }
        }

    }
}