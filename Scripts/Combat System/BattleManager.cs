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

        public BattleStateEnum battleState = BattleStateEnum.PREBATTLE;
        private TurnOrderManager turnOrder;
        private Array<Vector2I> walkableSpaces;
        [Export] private SkillSelection skillUI;
        [Export] private TurnDataManager turnDataManager;

        public TurnData castData;
        

        private double timeSinceStateStarted = 0;

        public CharacterSkill SelectedSkill { get; private set; }


        public override void _Ready()
        {
            base._Ready();
            var playerCharacters = game.playerParty;
            var enemyCharacters = game.enemyParty;
            playerParty = new Array<BattleCharacter>();
            enemyParty = new Array<BattleCharacter>();
            InitCharacters(playerCharacters, playerParty, playerStartingPositions);
            InitCharacters(enemyCharacters, enemyParty, enemyStartingPositions);

            turnOrder = new TurnOrderManager(playerParty, enemyParty);
        }

        public override void _ExitTree()
        {
            
        }

        public override void _Process(double delta)
        {
           
        }

        public bool IsPlayerPartyDead()
        {
            return IsPartyDead(playerParty);
        }

        public bool IsEnemyPartyDead()
        {
            return IsPartyDead(enemyParty);
        }

        private bool IsPartyDead(Array<BattleCharacter> party)
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
            castData = new TurnData(currentCharacter);
            grid.UpdateAllWalkableAreas(this, currentCharacter);
            currentCharacter.StartCharacterTurn(this, grid);
            skillUI.SetupButtons(currentCharacter);
        }

        public void ProgressTurn()
        {
            turnOrder.SetupNextTurn();
            turnDataManager.AddTurnData(castData);
            castData = null;
        }


        public bool IsInSameParty(BattleCharacter character, BattleCharacter target)
        {
            var party = GetCharacterParty(character);
            return party.Contains(target);
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
            currChar.SetPosition(space);
        }


        public void SelectAction(BattleCharacter caster, Array<BattleCharacter> targets, CharacterSkill skill)
        {
            castData = new TurnData(caster, targets, skill);
        }

        public void SetSelectedSkill(CharacterSkill skill)
        {
            SelectedSkill = skill;
            //OnSkillSelect.RaiseEvent(this, skill);
            //Get current space
        }


        #endregion

        /******************
         * Starting Positions
         * ****************/
        #region Starting Positions

        private void InitCharacters(Array<GameCharacter> characters, 
                                    Array<BattleCharacter> party, 
                                    Array<Vector2I> startingPositions)
        {
            for (int i = 0; i < characters.Count; i++)
            {
                var character = characters[i];
                var tempChar = Utils.InstantiateCopy<BattleCharacter>(battleCharacterScene);
                var startPos = startingPositions[i];
                tempChar.InitCharacter(character, startPos);
                OccupySpace(startPos, tempChar);

                AddChild(tempChar);
                party.Add(tempChar);
            }
        }

        #endregion

        /*****************
         * Helper functions
         * ***************/


        private void OccupySpace(Vector2I coords, BattleCharacter character)
        {
            var space = grid.GetSpaceFromCoords(coords);
            space.OccupySpace(character);
            character.Position = space.GlobalPosition;
        }


        public BattleGrid GetGrid()
        {
            return grid;
        }
    }
}