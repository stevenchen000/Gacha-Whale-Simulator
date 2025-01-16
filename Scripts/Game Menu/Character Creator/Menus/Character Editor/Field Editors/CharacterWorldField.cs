using Godot;
using System;
using Godot.Collections;
using EventSystem;

namespace CharacterCreator
{
    public partial class CharacterWorldField : CharacterFieldMenu
    {
        //CreateCharacterMenu menu
        [Export] private TextEdit textField;
        [Export] private ItemList listField;

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
            var newWorld = textField.Text;
            character.SetWorld(newWorld);
        }

        public override bool FieldValid()
        {
            return true;
        }

    }
}