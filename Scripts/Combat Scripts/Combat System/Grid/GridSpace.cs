using Godot;
using System;

namespace CombatSystem
{
    public partial class GridSpace : Node2D
    {
        [Export] public Color defaultColor { get; private set; }
        [Export] public Color allyColor { get; private set; }
        [Export] public Color allyAttackColor { get; private set; }
        [Export] public Color allyTargetColor { get; private set; }
        [Export] public Color allyCurrentTileColor { get; private set; }
        [Export] public Color enemyColor { get; private set; }
        [Export] public Color boundaryColor { get; private set; }

        [Export] private Sprite2D colliderSprite;
        [Export] private Sprite2D spaceSprite;


        private WeakReference _charRef;

        public BattleCharacter CharacterOnSpace {
            get {
                if (_charRef != null)
                    return (BattleCharacter)_charRef.Target;
                else
                    return null;
            }
            private set {
                if (value != null)
                    _charRef = new WeakReference(value);
                else
                    _charRef = null;
            } 
        }
        public Vector2I Coords { get; private set; }


        public bool IsWalkable { get; private set; }
        public int PartyIndex { get; private set; }
        public bool CanTarget { get; private set; }
        public bool HasSelectedTarget { get; private set; }

        private WeakReference<BattleManager> _battleRef;
        private BattleManager battle
        {
            get
            {
                BattleManager result = null;
                _battleRef.TryGetTarget(out result);
                return result;
            }
            set
            {
                _battleRef = new WeakReference<BattleManager>(value);
            }
        }


        public override void _Ready()
        {
            battle = Utils.FindParentOfType<BattleManager>(this);
        }

        public void InitSpace(Vector2I coords)
        {
            Coords = coords;
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
            
        }


        public void SelectSpace()
        {
            if (IsWalkable)
            {
                battle.SelectSpace(this);
            }
        }


        public void SetWalkable(bool walkable)
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

        

        public bool IsOccupied() { return CharacterOnSpace != null; }

        public void OccupySpace(BattleCharacter character)
        {
            if(CharacterOnSpace == null)
            {
                CharacterOnSpace = character;
            }
        }

        public void UnoccupySpace()
        {
            CharacterOnSpace = null;
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
            CanTarget = false;
            HasSelectedTarget = false;
        }

        public void SetCanTarget(bool canTarget)
        {
            CanTarget = canTarget;
        }

        public void SetHasSelectedTarget(bool hasSelectedTarget)
        {
            HasSelectedTarget = hasSelectedTarget;
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
    }
}