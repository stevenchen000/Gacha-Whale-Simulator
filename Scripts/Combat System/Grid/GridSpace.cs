using Godot;
using System;

namespace CombatSystem
{
    public partial class GridSpace : Node2D
    {
        private GridState state;
        [Export] private CollisionPolygon2D collider;
        [Export] private Color defaultColor;
        [Export] private Color allyColor;
        [Export] private Color allyAttackColor;
        [Export] private Color allyCurrentTileColor;
        [Export] private Color enemyColor;
        [Export] private Color boundaryColor;

        [Export] private Sprite2D colliderSprite;
        [Export] private Sprite2D spaceSprite;
        public BattleCharacter characterOnSpace { get; set; }

        private bool isWalkable;


        public bool IsWalkable()
        {
            return isWalkable;
        }

        private void SetWalkable(bool walkable)
        {
            isWalkable = walkable;

            if (walkable)
            {
                collider.Disabled = true;
                //colliderSprite.Visible = false;
            }
            else
            {
                collider.Disabled = false;
                //colliderSprite.Visible = true;
            }
        }

        public void SetState(GridState newState)
        {
            //GD.Print($"State set: {newState}");
            switch (newState)
            {
                case GridState.DEFAULT:
                    SetColor(defaultColor);
                    SetWalkable(false);
                    break;
                case GridState.ALLY_MOVEABLE:
                    SetColor(allyColor);
                    SetWalkable(true);
                    break;
                case GridState.ALLY_STANDING:
                    SetColor(allyCurrentTileColor);
                    SetWalkable(true);
                    break;
                case GridState.ALLY_ATTACK:
                    SetColor(allyAttackColor);
                    break;
                case GridState.ALLY_SUPPORT:
                    break;
                case GridState.ENEMY_MOVEABLE:
                    SetColor(enemyColor);
                    SetWalkable(true);
                    break;
                case GridState.ENEMY_STANDING:
                    break;
                case GridState.ENEMY_ATTACK:
                    break;
                case GridState.ENEMY_SUPPORT:
                    break;
                case GridState.BOUNDARY:
                    SetColor(boundaryColor);
                    SetWalkable(false);
                    break;
            }
            state = newState;
        }

        public bool IsOccupied() { return characterOnSpace != null; }

        public void OccupySpace(BattleCharacter character)
        {
            if(characterOnSpace == null)
            {
                characterOnSpace = character;
            }
        }

        public void EmptySpace()
        {
            characterOnSpace = null;
        }


        /****************
         * Helpers
         * ****************/

        private void SetColor(Color newColor)
        {
            spaceSprite.Modulate = newColor;
        }
    }
}