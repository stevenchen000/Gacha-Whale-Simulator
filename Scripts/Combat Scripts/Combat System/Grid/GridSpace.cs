using Godot;
using System;

namespace CombatSystem
{
    public partial class GridSpace : Node2D
    {
        [Export] public Color defaultColor { get; private set; }
        [Export] public Color allyColor { get; private set; }
        [Export] public Color enemyColor { get; private set; }
        [Export] public Color selectableColor { get; private set; }
        [Export] public Color attackColor { get; private set; }
        [Export] public Color healColor { get; private set; }



        [Export] private Sprite2D colliderSprite;
        [Export] private Sprite2D spaceSprite;

        [Export] private AnimationPlayer anim;



        public BattleCharacter CharacterOnSpace {
            get 
            {
                return grid.GetCharacterOnSpace(Coords);
            }
        }
        public Vector2I Coords { get; private set; }


        //State Data
        public bool IsWalkable { get; private set; } = false;
        public bool CanSelect { get; private set; } = false;
        public bool SelectionHasDirection { get; private set; } = false;
        public bool HasSelected { get; private set; } = false;
        public bool TargetIsEnemy { get; private set; } = false;
        public CharacterDirection DirectionSelection { get; private set; }

        private SimpleWeakRef<GridStateNode> _currState;
        private GridStateNode currState
        {
            get
            {
                if (_currState != null) return _currState.Value;
                else return null;
            }
            set
            {
                _currState = new SimpleWeakRef<GridStateNode> (value);
            }
        }




        private SimpleWeakRef<BattleManager> _battleRef;
        private BattleManager battle
        {
            get
            {
                return _battleRef.Value;
            }
            set
            {
                _battleRef = new SimpleWeakRef<BattleManager>(value);
            }
        }
        private SimpleWeakRef<BattleGrid> _grid;
        private BattleGrid grid
        {
            get
            {
                return _grid.Value;
            }
            set
            {
                _grid = new SimpleWeakRef<BattleGrid>(value);
            }
        }


        /***********************
         * Init
         * **************/

        public override void _Ready()
        {
            battle = Utils.FindParentOfType<BattleManager>(this);
            grid = battle.Grid;
            spaceSprite.Scale = new Vector2(0, 0);
        }

        public void InitSpace(Vector2I coords)
        {
            Coords = coords;
        }

        /// <summary>
        /// Used by GridStateNode to handle OnClick calls
        /// </summary>
        /// <param name="state"></param>
        public void SetState(GridStateNode state)
        {
            currState = state;
        }



        /******************
         * On Click
         * ***************/

        public void OnClick()
        {
            var caster = battle.GetCurrentCharacter();
            TargetingData targetingData = grid.CurrentTargetingData;
            TargetingSelection selection = grid.CurrentSelection;

            if (!battle.CharacterInPlayerParty(caster)) return;

            if(CanSelect && HasBeenSelected())
            {
                bool hasTarget = targetingData.ValidTargetInSelection(selection, grid);
                if(hasTarget) battle.ConfirmAction();
            }
            else if (CanSelect)
            {

                if (SelectionHasDirection)
                    selection = new TargetingSelection(DirectionSelection);
                else
                    selection = new TargetingSelection(Coords);

                battle.SetTargetSelection(selection);
            }
            else if (IsWalkable)
            {
                battle.OccupySpace(Coords, caster);
                caster.MoveAndUpdate(Coords);
                battle.SetTargetingData(null);
                battle.SetTargetSelection(null);
                battle.SetSelectedSkill(null);
            }
        }

        private bool HasBeenSelected()
        {
            var selection = grid.CurrentSelection;
            bool wasSelected = false;
            
            if (selection != null)
            {
                wasSelected = selection.HasSelectedSpace(this);
            }
            return wasSelected;
        }


        private void MoveToSpace(BattleCharacter character)
        {
            battle.OccupySpace(Coords, character);
            grid.SetTargetingData(null);
            grid.SetTargetingSelection(null);
        }


        /******************
         * Public Functions
         * ***************/


        

        public bool IsOccupied() { return CharacterOnSpace != null; }

        public void OccupySpace(BattleCharacter character)
        {
            grid.OccupySpace(character, Coords);
        }

        public void UnoccupySpace()
        {
            grid.UnoccupySpace(Coords);
        }

        public void EmptySpace()
        {
            UnoccupySpace();
        }

        public void SetColor(Color newColor)
        {
            spaceSprite.SelfModulate = newColor;
        }

        public void ResetSpace()
        {
            IsWalkable = false;
            CanSelect = false;
            SelectionHasDirection = false;
            HasSelected = false;
        }

        /**************
         * Set State
         * ************/

        public void SetWalkable(bool walkable)
        {
            IsWalkable = walkable;
        }

        public void SetCanSelect(bool canSelect)
        {
            CanSelect = canSelect;
        }

        public void SetSelectionHasDirection(CharacterDirection direction)
        {

            SelectionHasDirection = true;
            DirectionSelection = direction;
        }

        public void ResetSelectionHasDirection()
        {
            SelectionHasDirection = false;
        }

        public void SetHasSelected(bool hasSelected)
        {
            HasSelected = hasSelected;
        }



        /****************
         * Helpers
         * ****************/



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




        public void PlayAppearAnimation()
        {
            anim.Play("appear_in");
        }
    }
}