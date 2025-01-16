using Godot;
using System;
using Godot.Collections;
using EventSystem;

namespace CharacterCreator
{
    public partial class SelectionMenu : CharacterCreatorMenu
    {
        [Export] private CharacterDisplayUi display;
        [Export] private VoidEvent OnEditorPortraitChanged;
        [Export] private CharacterGridManager grid;

        /**********************
         * Main Functions
         * *******************/

        #region Main Functions

        public override void _Init(CharacterCreatorManager creator)
        {
            base._Init(creator);
            grid.LoadPortraits();
            _UpdateDisplay();
            //OnEditorPortraitChanged.SubscribeEvent(_UpdateDisplay);
        }

        public override void _OnDisable()
        {
            base._OnDisable();
            grid = null;
            display = null;
            //OnEditorPortraitChanged.UnsubscribeEvent(_UpdateDisplay);
        }

        public override void _Refresh()
        {
            base._Refresh();
            grid.LoadPortraits();
        }

        #endregion
        private void _UpdateDisplay()
        {
            var portrait = creator.GetPortrait();
            var character = creator.GetCharacter();
            if (portrait != null)
            {
                UpdateDisplay(portrait);
            }else if(character != null)
            {

            }
        }

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

        #region Button Functions

        public void ResetDisplay()
        {
            display.ResetDisplay();
        }

        public void OpenPortraitEditor()
        {
            creator.EnableEditPortraitMenu();
        }

        public void DeleteCurrentObject()
        {
            var portrait = creator.GetPortrait();
            if(portrait != null)
            {
                FileManager.DeletePortrait(portrait);
                creator.Reload();
            }

        }

        public void CreateCharacterFromPortrait()
        {
            var portrait = creator.GetPortrait();
            if(portrait != null)
            {
                var character = new CustomGameCharacter();
            }
        }

        #endregion
    }
}