using Godot;
using System;
using Godot.Collections;
using GachaSystem;
using EventSystem;

namespace CombatSystem
{
    public partial class BattleManager : GameMenu
    {
        public BattleGrid Grid
        {
            get
            {
                return State.Grid;
            }
        }

        [Export] private PackedScene roomScene;

        [Export] private SkillSelection skillUI;
        [Export] private DirectionButtonUI directionUI;
        [Export] private TurnDataManager turnDataManager;
        [Export] public Camera2D Camera { get; private set; }

        public TurnData turnData;

        [Export] public BattleState State { get; private set; }
        

        public SkillContainer SelectedSkill 
        {
            get
            {
                var targetData = Grid.CurrentTargetingData;
                SkillContainer skill = null;
                skill = targetData?.Skill;
                return skill;
            }
        }
        public TargetingData SkillTargeting { get; private set; }
        public TargetingSelection CurrentTargetSelection { get; private set; }
        public bool TurnConfirmed { get; private set; }


        [Signal]
        public delegate void TurnStartEventHandler();





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
            var result = State.TurnOrder.GetCurrentCharacter();
            return result;
        }

        public bool CharacterInPlayerParty(BattleCharacter character)
        {
            return State.PlayerParty.IsInParty(character);
        }

        /****************
         * Turn Functions
         * **************/

        #region Turn Functions

        public void StartTurn()
        {
            Utils.Print(this, GetCurrentCharacter());
            var currentCharacter = GetCurrentCharacter();
            currentCharacter.StartCharacterTurn(State);

            //if controlled character
            skillUI.SetupButtons(currentCharacter);
            HideConfirmationAndShowSkipButton();

            EmitSignal(SignalName.TurnStart);
        }

        public void ConfirmAction()
        {
            TurnConfirmed = true;
            SelectAction();
            Grid.ResetSpaces();
            skillUI.HideConfirmationButtons();
            skillUI.HideSkipButton();
        }

        public void EndTurn()
        {
            var character = GetCurrentCharacter();
            character.EndTurn(0, this, Grid);
            State.TurnOrder.SetupNextTurn();
            turnDataManager.AddTurnData(turnData);
            Grid.ResetGrid();
            turnData = null;
            TurnConfirmed = false;
        }

        public void SkipTurn()
        {
            TurnConfirmed = true;
            Grid.SetTargetingData(null);
            Grid.SetTargetingSelection(null);
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


        public void SelectAction()
        {
            var targeting = Grid.CurrentTargetingData;
            var selection = Grid.CurrentSelection;
            turnData = new TurnData(Grid, targeting, selection);
        }
        #endregion


        #region Set Skill Button

        public void SetSelectedSkill(SkillContainer skill)
        {
            if (skill != null)
            {
                var caster = GetCurrentCharacter();
                var currPos = caster.currPosition;
                var currSpace = Grid.GetSpaceFromCoords(currPos);
                var targetingData = skill.GetTargetingData(Grid, caster, currPos);

                Grid.SetTargetingData(targetingData);
            }
            else
            {
                Grid.SetTargetingData(null);
            }
        }



        public void SetTargetSelection(TargetingSelection selection)
        {
            Grid.SetTargetingSelection(selection);
        }





        #endregion
        /******************
         * Skill UI
         * *****************/

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

        public void ShowSkillUI()
        {
            skillUI.ShowUI();
        }

        public void HideSkillUI()
        {
            skillUI.HideUI();
        }



        public void CancelSelectedSkill()
        {
            SetSelectedSkill(null);
            //Need to redo this
            State.Grid.SetTargetingData(null);
            State.Grid.SetTargetingSelection(null);
            
            HideConfirmationAndShowSkipButton();
        }


        

        /// <summary>
        /// When a skill has been selected,
        /// will give number of possible directions
        /// that have valid targets
        /// </summary>
        /// <returns></returns>
        public int GetNumberOfTargetableDirections()
        {
            int result = Grid.CurrentTargetingData.GetValidDirections(Grid).Count;

            return result;
        }



        public void SelectDirection(CharacterDirection direction)
        {
            var selection = new TargetingSelection(direction);
            Grid.SetTargetingSelection(selection);
        }

        public void ResetDirection()
        {
            Grid.SetTargetingSelection(null);
        }





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
            var space = Grid.GetSpaceFromCoords(coords);

            if (space != null)
            {
                result = OccupySpace(space, character);
                //gray out skills out of range
                if(character == GetCurrentCharacter())
                    skillUI.UpdateIfButtonsShouldBeActive(this, Grid, GetCurrentCharacter());
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
                space.OccupySpace(character);
                character.SetTemporaryPosition(space);
            }

            return result;
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