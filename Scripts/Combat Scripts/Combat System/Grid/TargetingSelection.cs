using System;
using Godot;

namespace CombatSystem
{
    public class TargetingSelection
    {
        public TargetSelectionStyle Style { get; private set; }
        public CharacterDirection SelectedDirection { get; private set; }
        public Vector2I SelectedSpace { get; private set; }

        public TargetingSelection(CharacterDirection direction)
        {
            Style = TargetSelectionStyle.SelectDirection;
            SelectedDirection = direction;
        }

        public TargetingSelection(Vector2I space)
        {
            Style = TargetSelectionStyle.SelectSpace;
            SelectedSpace = space;
        }

        public TargetingSelection() 
        {
            Style = TargetSelectionStyle.None;
        }



        public bool HasSelectedSpace(GridSpace space)
        {
            bool hasSelected = false;
            switch (Style)
            {
                case TargetSelectionStyle.SelectSpace:
                    hasSelected = space.Coords == SelectedSpace;
                    break;
                case TargetSelectionStyle.SelectDirection:
                    hasSelected = space.DirectionSelection == SelectedDirection;
                    break;
            }
            return hasSelected;
        }
    }
}
