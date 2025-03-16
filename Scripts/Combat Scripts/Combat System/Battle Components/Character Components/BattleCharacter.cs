using System;
using Godot;
using GachaSystem;
using Godot.Collections;
using CharacterCreator;
using SkillSystem;

namespace CombatSystem
{
    public partial class BattleCharacter : Node2D
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

        //Character data
        public CharacterData Character { get; private set; }
        [Export] private CharacterPortraitDisplay portrait;


        //Movement Vars
        [Export] private Node2D characterPos;
        public Vector2I turnStartPosition { get; private set; } = Vector2I.MinValue;
        public Vector2I currPosition { get; private set; } = Vector2I.MinValue;
        public CharacterDirection facingDirection { get; private set; }
        [Export] public float speed = 5;
        public Array<Vector2I> WalkableSpaces { get; private set; } = new Array<Vector2I>();


        //Combat Vars
        [Export] public Array<CharacterSkill> skills { get; set; }
        [Export] private StatContainer savedStats { get; set; }
        [Export] public BattleStatsManager Stats { get; private set; }
        [Export] public EffectManager Status { get; private set; }
        [Export] public CombatAI AI { get; private set; }



        public Array<BattleCharacter> targets { get; private set; }
        public int PartyIndex { get; private set; }



        public override void _Ready()
        {
            battle = Utils.FindParentOfType<BattleManager>(this);
        }

        public override void _Process(double delta)
        {
            
        }


        public void InitCharacter(CharacterData character, BattleParty party)
        {
            Character = character;
            Party = party;
            Stats._Init(character);
            skills = character.GetSkills();
            CharacterPortrait charPortrait = character.GetPortrait();
            portrait.UpdatePortrait(charPortrait);

            AI = character.Character.EnemyAI;
        }



        /*************
         * Turn Functions
         * ************/


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

        public CombatActionData GetAutomaticAction()
        {
            CombatActionData result = null;

            if(AI != null)
                result = AI.CalculateAction(battle);

            return result;
        }

        /*****************
         * Movement
         * **************/

        public void SetTemporaryPosition(GridSpace space)
        {
            currPosition = space.Coords;
            characterPos.GlobalPosition = space.GlobalPosition;
        }

        public void SetPosition(GridSpace space)
        {
            turnStartPosition = space.Coords;
            currPosition = space.Coords;
            characterPos.GlobalPosition = space.GlobalPosition;
        }

        public new Vector2 GetPosition()
        {
            return characterPos.GlobalPosition;
        }

        /*******************
         * Status Effects
         * ****************/

        public void AddStatus(BattleCharacter caster, StatusEffect effect)
        {
            Status.AddEffect(caster, effect);
        }

        /*************
         * Helpers
         * *************/

        public bool IsDead() { return Stats.IsDead(); }


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
        /*
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
        }*/
    }
}
