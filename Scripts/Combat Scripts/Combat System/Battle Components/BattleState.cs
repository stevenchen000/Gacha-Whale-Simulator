using Godot;
using System;
using GachaSystem;
using Godot.Collections;
using static Godot.WebSocketPeer;

namespace CombatSystem
{
    public partial class BattleState : Node
    {
        //Stage
        public StageData CurrentStage { get; private set; }

        //Parties
        public BattleParty PlayerParty { get; private set; }
        public BattleParty EnemyParty { get; private set; }
        public BattleParty NeutralParty { get; private set; }

        //Turns
        public int TurnCount { get; private set; }
        [Export] public TurnOrderManager TurnOrder { get; private set; }
        public TurnDataManager TurnManager { get; private set; }
        public TurnData CurrentTurnData { get; private set; }
        public CharacterDamageManager DamageManager { get; private set; }


        //Grid
        [Export] public BattleGrid Grid { get; private set; }
        public Array<GridSpace> WalkableSpaces { get; private set; }
        public Dictionary<CharacterDirection, Array<GridSpace>> TargetableSpaces { get; private set; }


        //Character And Actions
        public BattleCharacter CurrentCharacter { get; private set; }
        public CharacterSkill CurrentSkill { get; private set; }
        public CharacterDirection CurrentDirection { get; private set; }

        public bool TurnConfirmed { get; private set; }

        [Export] private PackedScene battleCharacterScene;
        [Export] private PortraitBorder playerBorder;
        [Export] private PortraitBorder enemyBorder;



        public void _Init(StageData data)
        {
            CurrentStage = data;

            SetupGrid(data);
            SetupParties(data);
            SetupStartingPositions();
            TurnOrder.Init(this);
            DamageManager = new CharacterDamageManager();
        }

        private void SetupGrid(StageData data)
        {
            int x = data.GridSize.X;
            int y = data.GridSize.Y;
            Grid.InitGrid(x, y, this);
        }



        /*****************
         * Parties
         * **************/

        public Array<BattleCharacter> GetAllLivingCharacters()
        {
            var result = new Array<BattleCharacter>();
            result.AddRange(PlayerParty.GetAllLivingMembers());
            result.AddRange(EnemyParty.GetAllLivingMembers());
            return result;
        }

        public void UpdatePartyStatuses()
        {
            PlayerParty.CheckPartyHealth();
            EnemyParty.CheckPartyHealth();
        }



        private void SetupParties(StageData data)
        {
            if (!data.OverridePlayerParty)
            {
                var party = GameState.GetCurrentParty();
                var playerCharacters = party.Party;
                PlayerParty = new BattleParty(this, playerCharacters, battleCharacterScene, playerBorder);
            }
            var enemyCharacters = data.EnemyList;
            var playerStartPos = data.PlayerStartPositions;
            var enemyStartPos = data.EnemyStartPositions;

            EnemyParty = new BattleParty(this, enemyCharacters, battleCharacterScene, enemyBorder);

        }

        private void SetupStartingPositions()
        {
            var stage = CurrentStage;
            var playerParty = PlayerParty;
            var enemyParty = EnemyParty;
            var playerPositions = stage.PlayerStartPositions;
            var enemyPositions = stage.EnemyStartPositions;

            Utils.Print(this, "Setting up player party...");
            SetupPartyPositions(playerParty, playerPositions);
            Utils.Print(this, "Setting up enemy party...");
            SetupPartyPositions(enemyParty, enemyPositions);
        }

        private void SetupPartyPositions(BattleParty party, Array<Vector2I> positions)
        {
            var members = party.GetAllMembers();
            Utils.Print(this, members.Count);
            for (int i = 0; i < members.Count; i++)
            {
                Vector2I coords = new Vector2I(-1, -1);
                GridSpace space = null;

                if (positions.Count > i)
                {
                    coords = positions[i];
                    space = Grid.GetSpaceFromCoords(coords);
                }
                else
                {
                    Utils.Print(this, "Error, not enough starting positions given");
                }

                var character = members[i];
                character.SetPosition(space);
                space.OccupySpace(character);
            }
        }

    }
}