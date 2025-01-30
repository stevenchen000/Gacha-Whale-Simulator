using System;
using Godot;
using GachaSystem;
using Godot.Collections;
using CharacterCreator;

namespace CombatSystem
{
    public partial class BattleCharacter : CharacterBody2D
    {
        private GameCharacter characterData;
        [Export] private CharacterPortraitDisplay portrait;
        //[Export] private GachaCharacterData characterData;
        [Export] public int movableSpaces { get; set; } = 2;
        public Vector2I previousPosition { get; private set; }
        public CharacterDirection facingDirection { get; private set; }
        [Export] public float speed = 5;
        [Export] private CollisionShape2D collider;
        [Export] public CharacterSkill skill { get; set; }
        [Export] private StatContainer savedStats { get; set; }
        public BattleStats stats { get; set; }

        private GridSpace previousNearestSpace = null;
        private GridSpace previousAttackSpace = null;
        private Array<GridSpace> previousAttackArea = null;

        public CharacterSkill currSkill { get; private set; }
        public Array<BattleCharacter> targets { get; private set; }
        public Vector2I tempPosition { get; private set; }


        public override void _Ready()
        {
            //stats = new BattleStats(savedStats);
            //DisableCollider();
        }

        public void InitCharacter(GameCharacter character, Vector2I startPosition)
        {
            previousPosition = startPosition;
            characterData = character;
            skill = character.GetBasicAttack();
            //var charPortrait = character.GetPortrait();
            var charPortrait = FileManager.GetRandomPortrait();
            portrait.UpdatePortrait(charPortrait);
            //Utils.Print(this, "Using random portrait");
        }


        public void StartCharacterTurn(BattleManager battle, BattleGrid grid)
        {
            currSkill = null;
            targets = new Array<BattleCharacter>();
            tempPosition = previousPosition;
        }

        public bool ControlCharacter(double delta, BattleManager battle, BattleGrid grid)
        {
            bool selectedAction = false;
            var movement = CalculateMovementVector();
            SetFacingDirection(movement);
            MoveCharacter(movement);

            if (Input.IsActionJustPressed("ui_accept"))
            {
                currSkill = skill;
                targets.AddRange(grid.GetTargetsInRange(battle, this, currSkill.AttackArea));
                selectedAction = true;
            }

            return selectedAction;
        }


        public void EndTurn(double delta, BattleManager battle, BattleGrid grid)
        {
            //Position = Position.Lerp(previousNearestSpace.GlobalPosition, 1f);
            previousPosition = tempPosition;
        }

        public bool IsDead() { return stats.IsDead(); }


        public void DisableCollider()
        {
            //collider.Disabled = true;
        }

        public void EnableCollider()
        {
            //collider.Disabled = false;
        }

        public void SetPosition(GridSpace space)
        {
            Position = space.GlobalPosition;
            tempPosition = space.coords;
        }


        /***************
         * Helper Functions
         * ****************/

        private Vector2 GetMovementDirection()
        {
            float horizontal = 0;
            float vertical = 0;

            if (Input.IsActionPressed("ui_left"))
            {
                horizontal += -1;
            }
            if (Input.IsActionPressed("ui_right"))
            {
                horizontal += 1;
            }
            if (Input.IsActionPressed("ui_up"))
            {
                vertical += -1;
            }
            if (Input.IsActionPressed("ui_down"))
            {
                vertical += 1;
            }

            return new Vector2(horizontal, vertical);
        }
        
        private void SetFacingDirection(Vector2 movement)
        {
            float x = movement.X;
            if(x > 0)
            {
                facingDirection = CharacterDirection.RIGHT;
            }else if(x < 0)
            {
                facingDirection = CharacterDirection.LEFT;
            }
        }

        private void MoveCharacter(Vector2 movement)
        {
            Velocity = movement;
            MoveAndSlide();
        }

        private Vector2 CalculateMovementVector()
        {
            var direction = GetMovementDirection();
            var movement = direction.Normalized() * speed;
            return movement;
        }
    }
}
