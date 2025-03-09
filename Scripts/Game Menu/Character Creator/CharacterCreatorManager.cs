using Godot;
using EventSystem;
using System.Collections.Generic;
using System.Linq;

namespace CharacterCreator
{
    public partial class CharacterCreatorManager : GameMenu
    {

        //Current Selection
        private CustomCharacterPortrait currPortrait;
        private CustomGameCharacter currCharacter;
        [Export] private VoidEvent OnPortraitAdded;
        [Export] private VoidEvent OnEditorPortraitChanged;

        private List<CustomGameCharacter> _characterList;
        private List<CustomCharacterPortrait> _portraitList;
        private List<CustomCharacterPortrait> _unusedPortraitList;

        private Dictionary<string, CustomGameCharacter> _characterPortraitPaths;

        //filtered lists to use
        public List<CustomGameCharacter> characters;
        public List<CustomCharacterPortrait> portraits;

        [Export] private PackedScene displayMenuScene;
        [Export] private PackedScene editPortraitMenuScene;

        [Export] private CharacterCreatorMenu currMenu;


        /**********************
         * Main Functions
         * *******************/

        #region Main Functions

        public override void _Ready()
        {
            characters = new List<CustomGameCharacter>();
            portraits = new List<CustomCharacterPortrait>();
            CallDeferred(MethodName._InitCreator);
        }

        public override void _Init()
        {
            
        }

        public override void _ExitTree()
        {
            OnPortraitAdded.UnsubscribeEvent(Reload);
        }

        #endregion

        /*******************
         * Init
         * ***************/

        #region init

        private void _InitCreator()
        {
            InitCharacters();
            InitPortraits();
            SetupCharactersList();
            SetupPortraitsList();
            
            OnPortraitAdded.SubscribeEvent(Reload);
            currMenu?._Init(this);
        }

        private void InitPortraits()
        {
            var tempPortraits = CustomCharacterManager.GetAllPortraits();
            _portraitList = new List<CustomCharacterPortrait>();
            _portraitList.AddRange(tempPortraits);
            _portraitList = _portraitList.OrderByDescending(x => x.portraitFile).ToList();
            _unusedPortraitList = new List<CustomCharacterPortrait>();

            foreach (var portrait in _portraitList)
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
            var tempCharacters = CustomCharacterManager.GetAllCharacters();
            _characterList = new List<CustomGameCharacter>();
            _characterList.AddRange(tempCharacters);
            _characterList = _characterList.OrderByDescending(x => x.portraitFilename).ToList();
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
            EnableMenu(displayMenuScene);
        }

        public void EnableEditPortraitMenu()
        {
            EnableMenu(editPortraitMenuScene);
        }

        public void EnableMenu(PackedScene newScene)
        {
            if (currMenu != null)
            {
                currMenu._OnDisable();
                //RemoveChild(currMenu);
            }
            var newMenu = (CharacterCreatorMenu)Utils.InstantiateCopy(newScene);
            AddChild(newMenu);
            currMenu = newMenu;
            currMenu._Init(this);
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
            //displayMenu.UpdateDisplay(portrait);
            OnEditorPortraitChanged.RaiseEvent(this);
        }

        public void SetCharacter(CustomGameCharacter character)
        {
            currCharacter = character;
            currPortrait = null;
            OnEditorPortraitChanged.RaiseEvent(this);
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
            currMenu?._Refresh();
        }

        public void SetupCharactersList()
        {
            characters = new List<CustomGameCharacter>();
            characters.AddRange(_characterPortraitPaths.Values);
            
        }

        public void SetupPortraitsList()
        {
            portraits = new List<CustomCharacterPortrait>();
            portraits.AddRange(_unusedPortraitList);
        }





    }
}