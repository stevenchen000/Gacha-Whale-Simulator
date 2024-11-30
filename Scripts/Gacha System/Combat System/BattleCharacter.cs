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
        [Export] private GridShape attackShape;

        private GridSpace previousNearestSpace = null;
        private GridSpace previousAttackSpace = null;
        private Array<GridSpace> previousAttackArea = null;
        

        public void StartCharacterTurn(BattleManager battle, BattleGrid grid)
        {

        }

        public bool ControlCharacter(double delta, BattleManager battle, BattleGrid grid)
        {
            var movement = CalculateMovementVector();
            SetFacingDirection(movement);
            MoveCharacter(movement);
            UpdateNearestSpaceColor(grid);
            ShowSkillArea(grid);

            return false;
        }

        public void EndTurn(double delta, BattleManager battle, BattleGrid grid)
        {
            Position = Position.Lerp(previousNearestSpace.GlobalPosition, 1f);
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
            var attackPositions = attackShape.GetPositionsInRange(currentPosition, isFacingLeft);

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
