using Godot;
using System;

namespace CombatSystem
{
    public partial class GridSpace : Node2D
    {
        private GridState state;
        [Export] private CollisionShape2D collider;
        [Export] public Color defaultColor { get; private set; }
        [Export] public Color allyColor { get; private set; }
        [Export] public Color allyAttackColor { get; private set; }
        [Export] public Color allyCurrentTileColor { get; private set; }
        [Export] public Color enemyColor { get; private set; }
        [Export] public Color boundaryColor { get; private set; }

        [Export] private Sprite2D colliderSprite;
        [Export] private Sprite2D spaceSprite;

        [Export] private Button upButton;
        [Export] private Button downButton;
        [Export] private Button leftButton;
        [Export] private Button rightButton;


        public BattleCharacter characterOnSpace { get; private set; }
        public Vector2I coords { get; private set; }


        public bool IsWalkable { get; private set; }
        public int PartyIndex { get; private set; }
        public bool CanTarget { get; private set; }
        public bool HasSelectedTarget { get; private set; }

        private BattleManager battle;


        public override void _Ready()
        {
            battle = Utils.FindParentOfType<BattleManager>(this);

        }

        public void InitSpace(Vector2I coords)
        {
            this.coords = coords;
            HideButtons();
        }


        public void OnClick()
        {
            if (IsWalkable)
            {
                battle.SelectSpace(this);
            }
        }

        public void Test()
        {
            Utils.Print(this, coords);
        }


        public void SelectSpace()
        {
            if (IsWalkable)
            {
                battle.SelectSpace(this);
            }
        }

        public void ShowButtons(SkillDirection direction)
        {
            switch (direction)
            {
                case SkillDirection.ALL:
                    ShowAllButtons();
                    break;
                case SkillDirection.HORIZONTAL:
                    ShowHorizontalButtons();
                    break;
                case SkillDirection.VERTICAL:
                    ShowVerticalButtons();
                    break;
                case SkillDirection.NONE:
                    break;
            }
        }

        public void UpButtonClicked()
        {

        }

        public void DownButtonClicked()
        {

        }

        public void LeftButtonClicked()
        {

        }

        public void RightButtonClicked()
        {

        }

        private void SetWalkable(bool walkable)
        {
            IsWalkable = walkable;

            if (walkable)
            {
                //collider.Disabled = false;
                //colliderSprite.Visible = false;
            }
            else
            {
                //collider.Disabled = true;
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

        public void SetColor(Color newColor)
        {
            spaceSprite.SelfModulate = newColor;
        }

        /****************
         * Helpers
         * ****************/


        private void HideButtons()
        {
            DisableButton(upButton);
            DisableButton(downButton);
            DisableButton(leftButton);
            DisableButton(rightButton);
        }


        private void ShowAllButtons()
        {
            ShowHorizontalButtons();
            ShowVerticalButtons();
        }

        private void ShowHorizontalButtons()
        {
            EnableButton(leftButton);
            EnableButton(rightButton);
        }

        private void ShowVerticalButtons()
        {
            EnableButton(upButton);
            EnableButton(downButton);
        }

        private void DisableButton(Button button)
        {
            if (button != null)
            {
                button.Disabled = true;
                button.Visible = false;
            }
        }

        private void EnableButton(Button button)
        {
            if (button != null)
            {
                button.Disabled = false;
                button.Visible = true;
            }
        }
    }
}