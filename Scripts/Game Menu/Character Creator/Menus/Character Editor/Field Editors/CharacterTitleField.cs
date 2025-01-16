using Godot;
using System;
using Godot.Collections;
using EventSystem;

namespace CharacterCreator
{
    public partial class CharacterTitleField : CharacterFieldMenu
    {
        //CreateCharacterMenu menu
        [Export] private TextEdit textField;

        /**********************
         * Main Functions
         * *******************/

        #region Main Functions

        public override void _Init(CreateCharacterMenu menu)
        {
            base._Init(menu);
            var name = menu.character.Name;
            textField.Text = name;
        }

        public override void _Disable()
        {
            base._Disable();
        }

        #endregion

        public override void SaveData(CustomGameCharacter character)
        {
            var newTitle = textField.Text;
            character.SetTitle(newTitle);
        }

        public override bool FieldValid()
        {
            return true;
        }

    }
}