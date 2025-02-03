using System;
using Godot;
using GachaSystem;
using Godot.Collections;
using CharacterCreator;

namespace CombatSystem
{
    public partial class BattleCharacter : CharacterBody2D
    {
        private WeakReference<BattleManager> _battle;
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

        private GameCharacter characterData;
        [Export] private CharacterPortraitDisplay portrait;
        //[Export] private GachaCharacterData characterData;
        [Export] public int movableSpaces { get; set; } = 2;
        public Vector2I turnStartPosition { get; private set; } = Vector2I.MinValue;
        public CharacterDirection facingDirection { get; private set; }
        [Export] public float speed = 5;
        [Export] private CollisionShape2D collider;
        [Export] public CharacterSkill skill { get; set; }
        [Export] private StatContainer savedStats { get; set; }
        public BattleStats stats { get; private set; }

        //private GridSpace previousNearestSpace = null;
        //private GridSpace previousAttackSpace = null;
        //private Array<GridSpace> previousAttackArea = null;

        public CharacterSkill currSkill { get; private set; }
        public Array<BattleCharacter> targets { get; private set; }
        public Vector2I currPosition { get; private set; } = Vector2I.MinValue;
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
            var space = battle.GetGrid().GetSpaceFromCoords(currPosition);
            Position = Position.MoveToward(space.GlobalPosition, (float)delta * 3000);
        }


        public void InitCharacter(GameCharacter character, Vector2I startPosition, int partyIndex)
        {
            PartyIndex = partyIndex;
            turnStartPosition = startPosition;
            currPosition = startPosition;
            characterData = character;
            InitStats();
            skill = character.GetBasicAttack();
            var charPortrait = FileManager.GetRandomPortrait();
            portrait.UpdatePortrait(charPortrait);
        }


        public void StartCharacterTurn(BattleManager battle, BattleGrid grid)
        {
            currSkill = null;
            targets = new Array<BattleCharacter>();
            currPosition = turnStartPosition;
        }


        public void EndTurn(double delta, BattleManager battle, BattleGrid grid)
        {
            turnStartPosition = currPosition;
        }

        public bool IsDead() { return stats.IsDead(); }


        public void SetPosition(GridSpace space)
        {
            //Position = space.GlobalPosition;
            currPosition = space.Coords;
        }

        public bool IsEnemy(BattleCharacter target)
        {
            return target.PartyIndex != PartyIndex;
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


        /***************
         * Helper Functions
         * ****************/
        
        private void InitStats()
        {
            var role = characterData.Role;
            var roleStats = role.stats;
            stats = new BattleStats();

            stats.maxHealth = roleStats.GetMaxHealth(10);
            stats.currentHealth = stats.maxHealth;

            stats.attack = roleStats.GetAttack(10);
            stats.defense = roleStats.GetDefense(10);
            stats.speed = roleStats.GetSpeed(10);
        }
    }
}
