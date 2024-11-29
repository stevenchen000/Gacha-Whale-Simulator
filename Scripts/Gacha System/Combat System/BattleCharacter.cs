using System;
using Godot;
using GachaSystem;

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

        private GridSpace previousNearestSpace = null;

        public void StartCharacterTurn(BattleManager battle, BattleGrid grid)
        {

        }

        public void ControlCharacter(double delta, BattleManager battle, BattleGrid grid)
        {
            var direction = GetMovementDirection();
            var movement = direction.Normalized() * speed;
            //Position += movement;
            //grid needs to calculate nearest space based on position and offset per grid space
            var space = grid.GetNearestSpace(Position);
            UpdateNearestSpaceColor(space);
            Velocity = movement;
            MoveAndSlide();
            
        }

        public void EndTurn(double delta, BattleManager battle, BattleGrid grid)
        {
            Position = Position.Lerp(previousNearestSpace.GlobalPosition, 1f);
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
        
        private void UpdateNearestSpaceColor(GridSpace newSpace)
        {
            if (!newSpace.IsWalkable())
            {
                return;
            }

            if(previousNearestSpace != null)
            {
                previousNearestSpace.SetState(GridState.ALLY_MOVEABLE);
            }
            previousNearestSpace = newSpace;
            previousNearestSpace.SetState(GridState.ALLY_STANDING);
        }
    }
}
