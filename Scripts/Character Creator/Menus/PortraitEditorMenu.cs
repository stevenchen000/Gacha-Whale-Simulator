using Godot;
using System;
using Godot.Collections;
using EventSystem;

namespace CharacterCreator
{
    public partial class PortraitEditorMenu : CharacterCreatorMenu
    {
        private CustomCharacterPortrait originalPortrait;
        public CustomCharacterPortrait editedPortrait { get; private set; }

        [Export] private ImageCropper cropper;

        /**********************
         * Main Functions
         * *******************/

        #region Main Functions

        public override void _OnEnable()
        {
            base._OnEnable();
            var portrait = creator.GetPortrait();
            InitPortrait(portrait);
            
        }

        #endregion

        public void InitPortrait(CustomCharacterPortrait portrait)
        {
            originalPortrait = portrait;
            editedPortrait = (CustomCharacterPortrait)portrait.Duplicate();
            cropper.SetPortraitToEdit(editedPortrait);
        }

        private void SavePortrait()
        {
            var startPos = cropper.cropStart;
            float size = cropper.GetCropSize();
            originalPortrait.startArea = startPos;
            originalPortrait.size = size;
            GodotHelper.Print(this, "Saving file...");
            FileManager.SavePortrait(originalPortrait);
            GodotHelper.Print(this, "File saved!");
        }

        public void ReturnToDisplayMenu()
        {
            creator.EnableDisplayMenu();
            cropper.Disable();
        }

        public void SaveAndReturn()
        {
            SavePortrait();
            ReturnToDisplayMenu();
        }
    }
}