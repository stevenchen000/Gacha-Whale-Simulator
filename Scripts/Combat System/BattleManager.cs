using Godot;
using System;
using Godot.Collections;
using GachaSystem;

namespace CombatSystem
{
    public partial class BattleManager : Node
    {
        private Array<GachaCharacterData> playerPartyData { get; set; }
        private Array<GachaCharacterData> enemyPartyData { get; set; }

        [Export] private Array<BattleCharacter> playerParty;
        [Export] private Array<BattleCharacter> enemyParty;
        [Export] private BattleGrid grid;
        [Export] private Array<Vector2I> playerStartingPositions;
        [Export] private Array<Vector2I> enemyStartingPositions;

        [Export] private PackedScene roomScene;

        [Export] public SkillCaster skillCaster;

        private BattleCharacter currentCharacter;
        private Vector2I currentPosition;
        private Vector2I attackPosition;
        public BattleStateEnum battleState = BattleStateEnum.PREBATTLE;
        private TurnOrderManager turnOrder;
        private Array<Vector2I> walkableSpaces;

        public BattleSkillCastData castData;
        

        private double timeSinceStateStarted = 0;

        public override void _Ready()
        {
            InitStartingPositions();
            SetupStartingPositions(playerParty, playerStartingPositions);
            SetupStartingPositions(enemyParty, enemyStartingPositions);

            turnOrder = new TurnOrderManager(playerParty, enemyParty);
            currentCharacter = turnOrder.GetCurrentCharacter();
            currentPosition = currentCharacter.currentPosition;

        }

        public override void _Process(double delta)
        {
            /*switch (battleState)
            {
                case BattleStateEnum.PREBATTLE:
                    PreBattleState();
                    break;
                case BattleStateEnum.CHARACTER_TURN:
                    CharacterTurnState(delta);
                    break;
                case BattleStateEnum.CHARACTER_SELECT_ATTACK:

                    break;
                case BattleStateEnum.CHARACTER_ATTACK:
                    CharacterAttackState();
                    break;
                case BattleStateEnum.BATTLE_OVER:
                    BattleOverState();
                    break;
            }

            timeSinceStateStarted += delta;*/
        }

        public Array<BattleCharacter> FindCharactersInRange(Array<Vector2I> attackArea, BattleCharacter caster)
        {
            var characters = new Array<BattleCharacter>();
            characters.AddRange(playerParty);
            characters.AddRange(enemyParty);
            var result = new Array<BattleCharacter>();

            foreach(var character in characters)
            {
                var characterPosition = character.currentPosition;
                if (attackArea.Contains(characterPosition))
                {
                    result.Add(character);
                }
            }

            return result;
        }

        public bool IsPartyDead()
        {
            GD.Print("Party dead function hasn't been implemented yet!");
            return false;
        }

        public bool IsPartyVictor()
        {
            GD.Print("Party Victor function hasn't been implemented yet!");
            return true;
        }

        public void SelectAction(BattleCharacter caster, Array<BattleCharacter> targets, CharacterSkill skill)
        {
            castData = new BattleSkillCastData(caster, targets, skill);
        }



        /*****************
         * Public Functions
         * **************/

        public BattleCharacter GetCurrentCharacter()
        {
            return turnOrder.GetCurrentCharacter();
        }

        public void ProgressTurn()
        {
            turnOrder.SetupNextTurn();
        }

        public BattleGrid GetGrid()
        {
            return grid;
        }


        /*****************
		* Battle States
		* *************/

        private void PreBattleState()
        {
            GD.Print("Battle has started");
            ChangeState(BattleStateEnum.CHARACTER_TURN);
        }

        private void CharacterTurnState(double delta)
        {
            CheckCharacterMovement(delta);
        }

        private void CharacterMoveState()
        {
            //move character
            //select attack button
            //turn skip button
        }

        private void CharacterAttackState()
        {
            if (timeSinceStateStarted > 2)
            {
                bool playerPartyIsDead = CheckIfPartyIsDead(playerParty);
                bool enemyPartyIsDead = CheckIfPartyIsDead(enemyParty);
                GD.Print(playerPartyIsDead);
                GD.Print(enemyPartyIsDead);
                if (playerPartyIsDead)
                {
                    GD.Print("lol, u suk");
                    ChangeState(BattleStateEnum.BATTLE_OVER);
                }else if (enemyPartyIsDead)
                {
                    GD.Print("Congrats, you won!");
                    ChangeState(BattleStateEnum.BATTLE_OVER);
                }
                else
                {
                    ChangeState(BattleStateEnum.CHARACTER_TURN);
                }
            }
        }

        private void BattleOverState()
        {
            SceneManager.LoadScene("res://Scenes/Room.tscn");
        }

        /**********************
		 * State Transitions
		 * *******************/

        private void ChangeState(BattleStateEnum newState)
        {
            switch (battleState)
            {
                case BattleStateEnum.PREBATTLE:
                    break;
                case BattleStateEnum.CHARACTER_TURN:
                    break;
                case BattleStateEnum.CHARACTER_ATTACK:
                    break;
                case BattleStateEnum.BATTLE_OVER:
                    break;
            }

            switch (newState)
            {
                case BattleStateEnum.PREBATTLE:
                    break;
                case BattleStateEnum.CHARACTER_TURN:
                    currentCharacter = turnOrder.GetCurrentCharacter();
                    currentPosition = currentCharacter.currentPosition;

                    UpdateAllWalkableAreas();
                    //GD.Print($"{currentCharacter.Name} is taking their turn");
                    break;
                case BattleStateEnum.CHARACTER_ATTACK:
                    //GD.Print($"{currentCharacter.Name} is now attacking");
                    grid.SetAllSpacesToDefault();
                    turnOrder.SetupNextTurn();
                    break;
                case BattleStateEnum.BATTLE_OVER:
                    break;
            }

            timeSinceStateStarted = 0;
            battleState = newState;
        }


        /******************
         * Starting Positions
         * ****************/

        private void InitStartingPositions()
        {
            playerStartingPositions = new Array<Vector2I>();
            playerStartingPositions.Add(new Vector2I(0, 0));
            playerStartingPositions.Add(new Vector2I(0, 1));
            playerStartingPositions.Add(new Vector2I(0, 2));

            enemyStartingPositions = new Array<Vector2I>();
            enemyStartingPositions.Add(new Vector2I(3, 0));
            enemyStartingPositions.Add(new Vector2I(5, 1));
        }

        private void SetupStartingPositions(Array<BattleCharacter> party, Array<Vector2I> startingPositions)
        {
            for(int i = 0; i < party.Count; i++)
            {
                var character = party[i];
                var startingPosition = startingPositions[i];
                var space = grid.GetSpaceFromCoords(startingPosition);

                character.currentPosition = startingPosition;
                character.Position = space.GlobalPosition;
            }
        }


        /*****************
         * Helper functions
         * ***************/

        private bool CheckIfPartyIsDead(Array<BattleCharacter> party)
        {
            bool result = true;
            foreach(var character in party)
            {
                GD.Print($"{character.Name}");
                if (!character.IsDead())
                {
                    result = false;
                    break;
                }
            }
            return result;
        }

        private void CheckCharacterMovement(double delta)
        {
            bool turnFinished = currentCharacter.ControlCharacter(delta, this, grid);
            if (turnFinished)
            {
                currentCharacter.EndTurn(delta, this, grid);
                ChangeState(BattleStateEnum.CHARACTER_ATTACK);
            }
        }

        private void UpdateSpace(int x, int y)
        {
            bool isWalkable = CheckIfSpaceIsWalkable(x, y);

            if (isWalkable)
            {
                var space = grid.GetSpaceFromCoords(x, y);
                //GD.Print($"{x}, {y}");
                //GD.Print(space.Position);
                currentCharacter.Position = space.GlobalPosition;
                currentPosition = new Vector2I(x, y);
            }
        }

        public void UpdateAllWalkableAreas()
        {
            int movement = currentCharacter.movableSpaces;
            currentPosition = currentCharacter.currentPosition;

            GD.Print($"{currentCharacter.Name} starting at {currentPosition}");
            walkableSpaces = grid.RevealAllWalkableAreas(currentPosition, movement);
        }

        private bool CheckIfSpaceIsWalkable(int x, int y)
        {
            var coords = new Vector2I(x, y);
            return walkableSpaces.Contains(coords);
        }

    }
}