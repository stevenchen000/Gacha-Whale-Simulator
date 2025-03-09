using Godot;
using System;
using Godot.Collections;
using GachaSystem;
using EventSystem;

namespace CombatSystem
{
    public partial class BattleManager : GameMenu
    {
        [Export] private BattleGrid grid;

        [Export] private PackedScene roomScene;

        [Export] private SkillSelection skillUI;
        [Export] private DirectionButtonUI directionUI;
        [Export] private TurnDataManager turnDataManager;

        public TurnData turnData;

        [Export] public BattleState State { get; private set; }
        

        public CharacterSkill SelectedSkill 
        {
            get
            {
                return turnData.skill;
            }
            private set
            {
                turnData.skill = value;
            }
        }
        public CharacterDirection Direction
        {
            get
            {
                return turnData.direction;
            }
            private set
            {
                turnData.direction = value;
            }
        }
        public bool TurnConfirmed { get; private set; }
        public Dictionary<CharacterDirection, Array<GridSpace>> targetableSpaces;


        public override void _Ready()
        {
            base._Ready();
            Init();
        }

        public override void _OnDisable()
        {
            
        }

        private void Init()
        {
            var data = game.Value.StageData;
            State._Init(data);
        }

        public override void _ExitTree()
        {
            
        }

        public override void _Process(double delta)
        {
           
        }


        /*****************
         * Public Functions
         * **************/

        public BattleCharacter GetCurrentCharacter()
        {
            BattleCharacter result = State.TurnOrder.GetCurrentCharacter();

            return result;
        }

        /****************
         * Turn Functions
         * **************/

        #region Turn Functions

        public void StartTurn()
        {
            var currentCharacter = GetCurrentCharacter();
            turnData = new TurnData(currentCharacter);
            currentCharacter.StartCharacterTurn(State);
            grid.SetSpacesWalkable(currentCharacter);
            
            //if controlled character
            skillUI.SetupButtons(currentCharacter);
            HideConfirmationAndShowSkipButton();
        }

        public void ConfirmAction()
        {
            TurnConfirmed = true;
            grid.ResetSpaces();
            HideDirectionButtons();
            skillUI.HideConfirmationButtons();
            skillUI.HideSkipButton();
        }

        public void EndTurn()
        {
            var character = GetCurrentCharacter();
            character.EndTurn(0, this, grid);
            grid.ResetSpaces();
            State.TurnOrder.SetupNextTurn();
            turnDataManager.AddTurnData(turnData);
            turnData = null;
            TurnConfirmed = false;
        }




        public bool IsInSameParty(BattleCharacter character, BattleCharacter target)
        {
            return character.PartyIndex == target.PartyIndex;
        }


        #endregion

        /****************
         * Turn Actions
         * ***************/

        #region Turn Actions

        public void SelectSpace(GridSpace space)
        {
            var currChar = GetCurrentCharacter();
            grid.ResetHasSelectedTarget();
            grid.ResetCanTarget();
            OccupySpace(space, currChar);
        }

        public void SelectAction(BattleCharacter caster, Array<BattleCharacter> targets, CharacterSkill skill)
        {
            turnData = new TurnData(caster, targets, skill);
        }
        #endregion


        #region Set Skill Button

        public void SetSelectedSkill(CharacterSkill skill)
        {
            if (SelectedSkill == skill) return;

            var oldSkill = SelectedSkill;
            SelectedSkill = skill;
            var caster = GetCurrentCharacter();
            var currPos = caster.currPosition;
            var currSpace = grid.GetSpaceFromCoords(currPos);

            grid.ResetCanTarget();
            HideDirectionButtons();

            if (SelectedSkill != null)
            {
                targetableSpaces = SelectedSkill.GetAllTargetSpaces(this, grid, caster);
                grid.ShowAllTargetableAreas(targetableSpaces);
            }
        }

        public void ShowConfirmationAndHideSkipButton()
        {
            skillUI.ShowConfirmationButtons();
            skillUI.HideSkipButton();
        }

        public void HideConfirmationAndShowSkipButton()
        {
            skillUI.HideConfirmationButtons();
            skillUI.ShowSkipButton();
        }

        public void CancelSelectedSkill()
        {
            SetSelectedSkill(null);
            ResetDirection();
            grid.ResetCanTarget();
            grid.ResetHasSelectedTarget();
            HideConfirmationAndShowSkipButton();
        }

        public void HideDirectionButtons()
        {
            directionUI.HideButtons();
        }

        public void RevealDirectionButtons()
        {
            var caster = GetCurrentCharacter();
            var currPos = caster.currPosition;
            var currSpace = grid.GetSpaceFromCoords(currPos);

            directionUI.RevealButtons(currSpace, targetableSpaces);
        }

        public Array<CharacterDirection> GetValidDirections()
        {
            var result = new Array<CharacterDirection>();
            if(targetableSpaces != null)
                result.AddRange(targetableSpaces.Keys);
            return result;
        }

        /// <summary>
        /// When a skill has been selected,
        /// will give number of possible directions
        /// that have valid targets
        /// </summary>
        /// <returns></returns>
        public int GetNumberOfTargetableDirections()
        {
            int result = 0;
            
            if (targetableSpaces != null)
            {
                result = targetableSpaces.Keys.Count;
            }

            return result;
        }


        private bool ValidDirectionSelected()
        {
            bool result = false;

            if(targetableSpaces != null && targetableSpaces.ContainsKey(Direction))
            {
                result = true;
            }

            return result;
        }


        public void SelectDirection(CharacterDirection direction)
        {
            Direction = direction;
            var targets = targetableSpaces[direction];

            grid.ResetHasSelectedTarget();

            foreach(var target in targets)
            {
                target.SetHasSelectedTarget(true);
            }

            turnData.SetTargets(targetableSpaces[direction]);
        }

        public void ResetDirection()
        {
            Direction = CharacterDirection.NONE;
            turnData.SetTargets(null);
        }

        public void SkipTurn()
        {
            TurnConfirmed = true;
            SelectedSkill = null;
        }

        #endregion


        /******************
         * Starting Positions
         * ****************/
        #region Starting Positions
        /*
        private void InitCharacters(Array<CharacterData> characters, 
                                    Array<BattleCharacter> party, 
                                    int partyIndex,
                                    Array<Vector2I> startingPositions)
        {
            for (int i = 0; i < characters.Count; i++)
            {
                var character = characters[i];
                var tempChar = Utils.InstantiateCopy<BattleCharacter>(battleCharacterScene);
                var startPos = startingPositions[i];
                tempChar.InitCharacter(character, startPos, partyIndex);
                OccupySpace(startPos, tempChar);

                AddChild(tempChar);
                party.Add(tempChar);
            }
        }*/

        #endregion

        /*****************
         * Helper functions
         * ***************/

        /// <summary>
        /// Returns true if the new space is available
        /// Returns false if the new space is already occupied
        /// </summary>
        /// <param name="coords"></param>
        /// <param name="character"></param>
        /// <returns></returns>
        public bool OccupySpace(Vector2I coords, BattleCharacter character)
        {
            bool result = false;
            var space = grid.GetSpaceFromCoords(coords);
            HideDirectionButtons();

            if (space != null)
            {
                result = OccupySpace(space, character);
                //gray out skills out of range
                if(character == GetCurrentCharacter())
                    skillUI.UpdateIfButtonsShouldBeActive(this, grid, GetCurrentCharacter());
            }

            return result;
        }

        private bool OccupySpace(GridSpace space, BattleCharacter character)
        {
            var spaceChar = space.CharacterOnSpace;
            bool result = false;

            if (spaceChar == null)
            {
                result = true;
                UnoccupyPreviousSpace(character);
                space.OccupySpace(character);
                character.SetTemporaryPosition(space);
            }

            return result;
        }

        private void UnoccupyPreviousSpace(BattleCharacter character)
        {
            var prevPos = character.currPosition;
            if (prevPos != Vector2I.MinValue)
            {
                var prevSpace = grid.GetSpaceFromCoords(prevPos);
                prevSpace.UnoccupySpace();
            }
        }

        public BattleGrid GetGrid()
        {
            return State.Grid;
        }

        /*****************
         * Exit Scene
         * ***************/

        public void ReturnToCombatMenu()
        {
            game.Value.OpenStageSelectionMenu();
        }
    }
}