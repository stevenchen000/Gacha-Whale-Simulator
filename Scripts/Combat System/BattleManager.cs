using Godot;
using System;
using Godot.Collections;
using GachaSystem;
using EventSystem;

namespace CombatSystem
{
    public partial class BattleManager : GameMenu
    {
        [Export] private PackedScene battleCharacterScene;

        [Export] private CharacterSkillEvent OnSkillSelect;

        [Export] private Array<BattleCharacter> playerParty;
        [Export] private Array<BattleCharacter> enemyParty;
        [Export] private BattleGrid grid;
        [Export] private Array<Vector2I> playerStartingPositions;
        [Export] private Array<Vector2I> enemyStartingPositions;

        [Export] private PackedScene roomScene;

        private TurnOrderManager turnOrder;
        private Array<Vector2I> walkableSpaces;
        [Export] private SkillSelection skillUI;
        [Export] private DirectionButtonUI directionUI;
        [Export] private TurnDataManager turnDataManager;

        public TurnData turnData;
        

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
                turnData.SetTargets(targetableSpaces[value]);
            }
        }
        public bool TurnConfirmed { get; private set; }
        public Dictionary<CharacterDirection, Array<GridSpace>> targetableSpaces;


        public override void _Ready()
        {
            base._Ready();
            Init();
            enemyParty[0].Name = "Enemy 1";
            playerParty[1].Name = "Player 2";
        }

        private void Init()
        {
            var playerCharacters = game.playerParty;
            var enemyCharacters = game.enemyParty;
            playerParty = new Array<BattleCharacter>();
            enemyParty = new Array<BattleCharacter>();
            grid.InitGrid();
            InitCharacters(playerCharacters, playerParty, 0, playerStartingPositions);
            InitCharacters(enemyCharacters, enemyParty, 1, enemyStartingPositions);

            turnOrder = new TurnOrderManager(playerParty, enemyParty);
        }

        public override void _ExitTree()
        {
            
        }

        public override void _Process(double delta)
        {
           
        }

        /// <summary>
        /// 0 = player party
        /// 1 = enemy party
        /// </summary>
        /// <param name="party"></param>
        /// <returns></returns>
        public bool IsPartyDead(int party)
        {
            bool result = false;

            switch (party)
            {
                case 0:
                    result = _IsPartyDead(playerParty);
                    break;
                case 1:
                    result = _IsPartyDead(enemyParty);
                    break;
            }

            return result;
        }



        private bool _IsPartyDead(Array<BattleCharacter> party)
        {
            bool result = true;
            foreach(var ally in party)
            {
                if (!ally.IsDead())
                {
                    result = false;
                    break;
                }
            }
            return result;
        }

        public bool IsEnemy(BattleCharacter caster, BattleCharacter target)
        {
            Array<BattleCharacter> casterParty = GetCharacterParty(caster);
            Array<BattleCharacter> targetParty = GetCharacterParty(target);

            return casterParty != targetParty;
        }


        /*****************
         * Public Functions
         * **************/

        public BattleCharacter GetCurrentCharacter()
        {
            return turnOrder.GetCurrentCharacter();
        }

        /****************
         * Turn Functions
         * **************/

        #region Turn Functions

        public void StartTurn()
        {
            var currentCharacter = GetCurrentCharacter();
            turnData = new TurnData(currentCharacter);
            grid.UpdateAllWalkableAreas(this, currentCharacter);
            currentCharacter.StartCharacterTurn(this, grid);
            skillUI.SetupButtons(currentCharacter);
        }

        public void ConfirmAction()
        {
            TurnConfirmed = true;
            grid.ResetSpaces();
            directionUI.HideButtons();
        }

        public void EndTurn()
        {
            var character = GetCurrentCharacter();
            character.EndTurn(0, this, grid);
            grid.ResetSpaces();
            turnOrder.SetupNextTurn();
            turnDataManager.AddTurnData(turnData);
            turnData = null;
            TurnConfirmed = false;
        }




        public bool IsInSameParty(BattleCharacter character, BattleCharacter target)
        {
            return character.PartyIndex == target.PartyIndex;
        }

        public Array<BattleCharacter> GetCharacterParty(BattleCharacter character)
        {
            Array<BattleCharacter> result = null;

            if (playerParty.Contains(character))
            {
                result = playerParty;
            }else if (enemyParty.Contains(character))
            {
                result = enemyParty;
            }

            return result;
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

        public void SetSelectedSkill(CharacterSkill skill)
        {
            var oldSkill = SelectedSkill;
            SelectedSkill = skill;
            var caster = GetCurrentCharacter();
            var currPos = caster.currPosition;
            var currSpace = grid.GetSpaceFromCoords(currPos);

            if(oldSkill != null)
            {
                //grid remove all old skill targeting
                grid.ResetCanTarget();
            }

            if(SelectedSkill != null)
            {
                //Add targeting for new skill
                targetableSpaces = SelectedSkill.GetAllTargetSpaces(this, grid, caster);
                grid.ShowAllTargetableAreas(targetableSpaces);
                directionUI.RevealButtons(currSpace, targetableSpaces);
            }
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

        private void InitCharacters(Array<GameCharacter> characters, 
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
        }

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

            if (space != null)
            {
                result = OccupySpace(space, character);
            }

            return result;
        }

        private bool OccupySpace(GridSpace space, BattleCharacter character)
        {
            var spaceChar = space.CharacterOnSpace;
            bool result = false;
            Utils.Print(this, spaceChar);

            if (spaceChar == null)
            {
                result = true;
                UnoccupyPreviousSpace(character);
                space.OccupySpace(character);
                character.SetPosition(space);
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
            return grid;
        }
    }
}