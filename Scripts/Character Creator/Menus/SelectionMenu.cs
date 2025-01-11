using Godot;
using System;
using Godot.Collections;
using EventSystem;

namespace CharacterCreator
{
    public partial class SelectionMenu : CharacterCreatorMenu
    {
        [Export] private CharacterDisplayUi display;

        /**********************
         * Main Functions
         * *******************/

        #region Main Functions

        public override void _Process(double delta)
        {
            
        }

        public override void _OnEnable()
        {
            base._OnEnable();
        }

        public override void _OnDisable()
        {
            base._OnDisable();
        }

        #endregion

        public void SetCreatorPortrait(CustomCharacterPortrait portrait)
        {
            creator.SetPortrait(portrait);
            UpdateDisplay(portrait);
        }

        public void SetCreatorCharacter(CustomGameCharacter character)
        {
            creator.SetCharacter(character);
            var portrait = (CustomCharacterPortrait)character.GetPortrait();
            UpdateDisplay(portrait);
        }


        public void UpdateDisplay(CustomCharacterPortrait portrait)
        {
            display.UpdateDisplay(portrait);
        }

        public void OpenPortraitEditor()
        {
            creator.EnableEditPortraitMenu();
        }
    }
}