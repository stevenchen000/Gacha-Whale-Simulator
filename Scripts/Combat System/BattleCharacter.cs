using System;
using Godot;
using GachaSystem;
using Godot.Collections;

namespace CombatSystem
{
    public partial class BattleCharacter : CharacterBody2D
    {
        [Export] private GachaCharacterData characterData;
        [Export] public int movableSpaces { get; set; } = 2;
        private Vector2I previousPosition { get; set; }
        public Vector2I currentPosition { get; set; }
        public bool isFacingLeft { get; set; }
        [Export] public float speed = 5;
        //[Export] private GridShape attackShape;
        [Export] public CharacterSkill skill { get; set; }
        [Export] private StatContainer savedStats { get; set; }
        public BattleStats stats { get; set; }

        private GridSpace previousNearestSpace = null;
        private GridSpace previousAttackSpace = null;
        private Array<GridSpace> previousAttackArea = null;

        public CharacterSkill currSkill { get; private set; }
        public Array<BattleCharacter> targets { get; private set; }


        public override void _Ready()
        {
            stats = new BattleStats(savedStats);
        }


        public void StartCharacterTurn(BattleManager battle, BattleGrid grid)
        {
            currSkill = null;
            targets = new Array<BattleCharacter>();
        }

        public bool ControlCharacter(double delta, BattleManager battle, BattleGrid grid)
        {
            bool selectedAction = false;
            var movement = CalculateMovementVector();
            SetFacingDirection(movement);
            MoveCharacter(movement);
            UpdateNearestSpaceColor(grid);
            ShowSkillArea(grid);

            if (Input.IsActionJustPressed("ui_accept"))
            {
                currSkill = skill;
                targets.AddRange(GetTargetsInRange(battle, grid));
                selectedAction = true;
            }

            return selectedAction;
        }

        public void EndTurn(double delta, BattleManager battle, BattleGrid grid)
        {
            Position = Position.Lerp(previousNearestSpace.GlobalPosition, 1f);
        }

        public bool IsDead() { return stats.IsDead(); }

        


        /***************
         * Helper Functions
         * ****************/
        private void AttackTarget(BattleManager battle, BattleGrid grid)
        {
            var attackPositions = skill.attackArea.GetPositionsInRange(currentPosition, isFacingLeft);
            var targets = battle.FindCharactersInRange(attackPositions, this);
            GD.Print(targets.Count);
            foreach(var target in targets)
            {
                int potency = skill.potency;
                target.stats.TakeDamage(stats, potency);
            }
        }

        private Array<BattleCharacter> GetTargetsInRange(BattleManager battle, BattleGrid grid)
        {
            var attackPositions = skill.attackArea.GetPositionsInRange(currentPosition, isFacingLeft);
            var targets = battle.FindCharactersInRange(attackPositions, this);
            return targets;
        }

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
        
        private void UpdateNearestSpaceColor(BattleGrid grid)
        {
            Vector2I newPosition = grid.CalculateNearestGridSpace(Position);
            var newSpace = grid.GetSpaceFromCoords(newPosition);

            if (newSpace != null && !newSpace.IsWalkable())
            {
                return;
            }

            if(previousNearestSpace != null)
            {
                previousNearestSpace.SetState(GridState.ALLY_MOVEABLE);
            }
            previousNearestSpace = newSpace;
            previousNearestSpace.SetState(GridState.ALLY_STANDING);

            previousPosition = currentPosition;
            currentPosition = newPosition;
        }

        private void SetFacingDirection(Vector2 movement)
        {
            float x = movement.X;
            if(x > 0)
            {
                isFacingLeft = false;
            }else if(x < 0)
            {
                isFacingLeft = true;
            }

            //GD.Print($"X Vel: {x}; Facing left: {isFacingLeft}");
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

        private void ShowSkillArea(BattleGrid grid)
        {
            var attackArea = skill.attackArea;
            var attackPositions = attackArea.GetPositionsInRange(currentPosition, isFacingLeft);

            if(previousAttackArea != null)
            {
                foreach(var space in previousAttackArea)
                {
                    if (space.IsWalkable()) space.SetState(GridState.ALLY_MOVEABLE);
                    else space.SetState(GridState.DEFAULT);
                }
            }

            previousAttackArea = new Array<GridSpace>();
            foreach(var position in attackPositions)
            {
                var space = grid.GetSpaceFromCoords(position);
                if(space != null)
                {
                    previousAttackArea.Add(space);
                    space.SetState(GridState.ALLY_ATTACK);
                }
            }
        }
    }
}
