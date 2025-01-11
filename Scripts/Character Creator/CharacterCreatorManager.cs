using Godot;
using System;
using Godot.Collections;
using EventSystem;

namespace CharacterCreator
{
    public partial class CharacterCreatorManager : Control
    {

        //Current Selection
        private CustomCharacterPortrait currPortrait;
        private CustomGameCharacter currCharacter;
        [Export] private VoidEvent OnPortraitAdded;

        private Array<CustomGameCharacter> _characterList;
        private Array<CustomCharacterPortrait> _portraitList;
        private Array<CustomCharacterPortrait> _unusedPortraitList;

        private Dictionary<string, CustomGameCharacter> _characterPortraitPaths;

        //filtered lists to use
        public Array<CustomGameCharacter> characters;
        public Array<CustomCharacterPortrait> portraits;

        [Export] private SelectionMenu displayMenu;
        [Export] private PortraitEditorMenu editPortraitMenu;

        private CharacterCreatorMenu currMenu;


        /**********************
         * Godot Functions
         * *******************/

        #region Godot Functions

        public override void _Ready()
        {
            characters = new Array<CustomGameCharacter>();
            portraits = new Array<CustomCharacterPortrait>();
            currMenu = displayMenu;
            CallDeferred(MethodName._Init);
            CallDeferred(MethodName.RemoveChild, editPortraitMenu);
        }

        public override void _ExitTree()
        {
            //if(GetParent() == null)
                //OnPortraitAdded.UnsubscribeEvent(Reload);
        }

        #endregion

        /*******************
         * Init
         * ***************/

        #region init

        private void _Init()
        {
            InitCharacters();
            InitPortraits();
            SetupCharactersList();
            SetupPortraitsList();
            OnPortraitAdded.RaiseEvent(this);
            OnPortraitAdded.SubscribeEvent(Reload);
        }

        private void InitPortraits()
        {
            _portraitList = FileManager.GetAllPortraits();
            _unusedPortraitList = new Array<CustomCharacterPortrait>();

            foreach(var portrait in _portraitList)
            {
                string path = portrait.ResourcePath;
                if (!_characterPortraitPaths.ContainsKey(path))
                {
                    _unusedPortraitList.Add(portrait);
                }
            }
        }

        private void InitCharacters()
        {
            _characterList = FileManager.GetAllCharacters();
            _characterPortraitPaths = new Dictionary<string, CustomGameCharacter>();

            foreach(var character in _characterList)
            {
                string path = character.GetPortrait().ResourcePath;
                _characterPortraitPaths[path] = character;
            }
        }

        #endregion

        /*********************
         * Menus
         * ******************/

        #region menu

        public void EnableDisplayMenu()
        {
            EnableMenu(displayMenu);
        }

        public void EnableEditPortraitMenu()
        {
            EnableMenu(editPortraitMenu);
            editPortraitMenu.InitPortrait(GetPortrait());
        }

        public void EnableMenu(CharacterCreatorMenu newMenu)
        {
            if (currMenu != null)
            {
                currMenu._OnDisable();
                RemoveChild(currMenu);
            }
            AddChild(newMenu);
            currMenu = newMenu;
            currMenu._OnEnable();
            newMenu.Visible = true;
        }

        #endregion


        /*****************
         * Portrait/Character Getters
        ***************/

        #region Getters/Setters

        public void SetPortrait(CustomCharacterPortrait portrait)
        {
            currPortrait = portrait;
            currCharacter = null;
            displayMenu.UpdateDisplay(portrait);
        }

        public void SetCharacter(CustomGameCharacter character)
        {
            currCharacter = character;
            currPortrait = null;
        }

        public CustomCharacterPortrait GetPortrait()
        {
            if (currPortrait != null)
                return currPortrait;
            else if (currCharacter != null)
                return (CustomCharacterPortrait)currCharacter.GetPortrait();
            else
                return null;
        }

        public CustomGameCharacter GetCharacter()
        {
            return currCharacter;
        }

        #endregion


        public void Reload()
        {
            InitCharacters();
            InitPortraits();
            SetupCharactersList();
            SetupPortraitsList();
        }

        public void SetupCharactersList()
        {
            characters = new Array<CustomGameCharacter>();
            characters.AddRange(_characterPortraitPaths.Values);
        }

        public void SetupPortraitsList()
        {
            portraits = new Array<CustomCharacterPortrait>();
            portraits.AddRange(_unusedPortraitList);
        }





    }
}