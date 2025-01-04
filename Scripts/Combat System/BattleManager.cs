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

        public BattleStateEnum battleState = BattleStateEnum.PREBATTLE;
        private TurnOrderManager turnOrder;
        private Array<Vector2I> walkableSpaces;
        [Export] private TurnDataManager turnDataManager;

        public BattleSkillCastData castData;
        

        private double timeSinceStateStarted = 0;

        public override void _Ready()
        {
            InitStartingPositions();
            SetupStartingPositions(playerParty, playerStartingPositions);
            SetupStartingPositions(enemyParty, enemyStartingPositions);

            turnOrder = new TurnOrderManager(playerParty, enemyParty);
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
            turnDataManager.AddTurnData(castData);
            castData = null;
        }

        public BattleGrid GetGrid()
        {
            return grid;
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
                space.OccupySpace(character);

                character.Position = space.GlobalPosition;
            }
        }


        /*****************
         * Helper functions
         * ***************/

    }
}