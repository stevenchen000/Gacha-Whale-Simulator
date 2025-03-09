using System;
using Godot;
using GachaSystem;
using Godot.Collections;
using CharacterCreator;

namespace CombatSystem
{
    public partial class BattleCharacter : Node2D
    {
        private WeakReference<BattleManager> _battle;
        private SimpleWeakRef<BattleParty> _party { get; set; }
        public BattleParty Party 
        { 
            get 
            { 
                return _party.Value; 
            }
            private set
            {
                _party = new SimpleWeakRef<BattleParty> (value);
            }
        }
        private BattleManager battle
        {
            get
            {
                BattleManager result = null;
                _battle.TryGetTarget(out result);
                return result;
            }
            set
            {
                _battle = new WeakReference<BattleManager>(value);
            }
        }

        //Character data
        public CharacterData Character { get; private set; }
        [Export] private CharacterPortraitDisplay portrait;


        //Movement Vars
        [Export] public int movableSpaces { get; set; } = 2;
        public Vector2I turnStartPosition { get; private set; } = Vector2I.MinValue;
        public Vector2I currPosition { get; private set; } = Vector2I.MinValue;
        public CharacterDirection facingDirection { get; private set; }
        [Export] public float speed = 5;
        public Array<Vector2I> WalkableSpaces { get; private set; } = new Array<Vector2I>();


        //Combat Vars
        [Export] public Array<CharacterSkill> skills { get; set; }
        [Export] private StatContainer savedStats { get; set; }
        public BattleStats stats { get; private set; }



        public Array<BattleCharacter> targets { get; private set; }
        public int PartyIndex { get; private set; }



        public override void _Ready()
        {
            battle = Utils.FindParentOfType<BattleManager>(this);
        }

        public override void _Process(double delta)
        {
            MoveTowardsPosition(delta);
        }

        private void MoveTowardsPosition(double delta)
        {
            var space = battle.State.Grid.GetSpaceFromCoords(currPosition);
            var toPos = space.GlobalPosition;
            Position = Position.Lerp(toPos, (float)delta * 10);
            //Position = Position.MoveToward(space.GlobalPosition, (float)delta * 3000);
        }


        public void InitCharacter(CharacterData character, BattleParty party)
        {
            Character = character;
            Party = party;
            InitStats();
            skills = character.GetSkills();
            CharacterPortrait charPortrait = character.GetPortrait();
            portrait.UpdatePortrait(charPortrait);
        }


        public void StartCharacterTurn(BattleState battleState)
        {
            var grid = battleState.Grid;
            WalkableSpaces = grid.GetAllWalkableAreas(this);
            targets = new Array<BattleCharacter>();
            currPosition = turnStartPosition;
        }


        public void EndTurn(double delta, BattleManager battle, BattleGrid grid)
        {
            WalkableSpaces = null;
            turnStartPosition = currPosition;
        }

        public bool IsDead() { return stats.IsDead(); }

        public void SetTemporaryPosition(GridSpace space)
        {
            currPosition = space.Coords;
        }

        public void SetPosition(GridSpace space)
        {
            turnStartPosition = space.Coords;
            currPosition = space.Coords;
        }

        public bool IsEnemy(BattleCharacter target)
        {
            return target.Party != Party;
        }

        public bool MoveAndUpdate(Vector2I position)
        {
            bool moved = battle.OccupySpace(position, this);
            if (moved)
            {
                currPosition = position;
                turnStartPosition = position;
            }

            return moved;
        }

        public void SetParty(BattleParty party)
        {
            Party = party;
        }

        /***************
         * Helper Functions
         * ****************/
        
        private void InitStats()
        {
            var role = Character.Character.Role;
            var roleStats = role.stats;
            stats = new BattleStats();
            int level = Character.Level;

            stats.maxHealth = roleStats.GetMaxHealth(level);
            stats.currentHealth = stats.maxHealth;

            stats.attack = roleStats.GetAttack(level);
            stats.defense = roleStats.GetDefense(level);
            stats.speed = roleStats.GetSpeed(level);

            stats.movement = Character.Character.GetMovement();
        }
    }
}
