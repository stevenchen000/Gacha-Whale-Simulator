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

        private BattleCharacter currentCharacter;
        private Vector2I currentPosition;
        private Vector2I attackPosition;
        public BattleStateEnum battleState = BattleStateEnum.PREBATTLE;
        private TurnOrderManager turnOrder;
        private Array<Vector2I> walkableSpaces;

        private double timeSinceStateStarted = 0;

        public override void _Ready()
        {
            GD.Print("Battle Manager running");
            InitStartingPositions();
            SetupStartingPositions(playerParty, playerStartingPositions);
            SetupStartingPositions(enemyParty, enemyStartingPositions);

            turnOrder = new TurnOrderManager(playerParty, enemyParty);
            currentCharacter = turnOrder.GetCurrentCharacter();
            currentPosition = currentCharacter.currentPosition;

        }

        public override void _Process(double delta)
        {
            switch (battleState)
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

            timeSinceStateStarted += delta;
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
                ChangeState(BattleStateEnum.CHARACTER_TURN);
            }
        }

        private void BattleOverState()
        {

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
                    currentCharacter.EndTurn(0, this, grid);
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
                    GD.Print($"{currentCharacter.Name} is taking their turn");
                    break;
                case BattleStateEnum.CHARACTER_ATTACK:
                    GD.Print($"{currentCharacter.Name} is now attacking");
                    grid.SetAllSpacesToDefault();
                    turnOrder.SetupNextTurn(currentPosition);
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
            enemyStartingPositions.Add(new Vector2I(5, 0));
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

        private void CheckCharacterMovement(double delta)
        {
            bool turnFinished = currentCharacter.ControlCharacter(delta, this, grid);
            if (Input.IsActionJustPressed("ui_accept"))
            {
                currentCharacter.EndTurn(delta, this, grid);
                ChangeState(BattleStateEnum.CHARACTER_ATTACK);
            }
            /*int x = currentPosition.X;
            int y = currentPosition.Y;

            if (Input.IsActionJustReleased("ui_up"))
            {
                y++;
                UpdateSpace(x, y);
            }
            else if (Input.IsActionJustReleased("ui_down"))
            {
                y--;
                UpdateSpace(x, y);
            }
            else if (Input.IsActionJustReleased("ui_left"))
            {
                x--;
                UpdateSpace(x, y);
            }
            else if (Input.IsActionJustReleased("ui_right"))
            {
                x++;
                UpdateSpace(x, y);
            }else if (Input.IsActionJustReleased("ui_accept"))
            {
                ChangeState(BattleStateEnum.CHARACTER_ATTACK);
            }*/
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

        private void UpdateAllWalkableAreas()
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