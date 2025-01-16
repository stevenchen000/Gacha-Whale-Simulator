using Godot;
using System;
using Godot.Collections;
using EventSystem;

namespace CharacterCreator
{
    public partial class CreateCharacterMenu : CharacterCreatorMenu
    {
        [Export] private Array<CharacterFieldMenu> _fieldsMenus;
        public CustomGameCharacter character { get; private set; }

        private int index = 0;

        /**********************
         * Main Functions
         * *******************/

        #region Main Functions


        public override void _Init(CharacterCreatorManager creator)
        {
            base._Init(creator);
            character = new CustomGameCharacter();
            InitFields();
            //portrait
            //name
            //title
            //world
            //element
            //class
            //role
            //description
        }

        public override void _OnDisable()
        {
            base._OnDisable();
            
        }

        #endregion

        /***************
         * Public Functions
         * *************/

        public void ReturnToPreviousMenu()
        {
            if(index > 0)
            {
                var currMenu = _fieldsMenus[index];
                var prevMenu = _fieldsMenus[index - 1];
                currMenu.CancelMenu();
                prevMenu.ActivateMenu();
                index--;
            }
        }

        public void GoToNextMenu()
        {
            if (index < _fieldsMenus.Count - 1)
            {
                var currMenu = _fieldsMenus[index];
                var nextMenu = _fieldsMenus[index + 1];
                if (currMenu.FieldValid())
                {
                    currMenu.CompleteMenu();
                    nextMenu.ActivateMenu();
                    index++;
                }
            }
        }

        public void FinishEditing()
        {
            if (_fieldsMenus[-1].FieldValid())
            {
                foreach (var field in _fieldsMenus)
                {
                    field.SaveData(character);
                }
            }
        }









        private void InitFields()
        {
            foreach(var menu in _fieldsMenus)
            {
                menu._Init(this);
            }
        }



    }
}