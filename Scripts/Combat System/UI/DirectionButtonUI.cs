using Godot;
using System;
using Godot.Collections;

namespace CombatSystem
{
    public partial class DirectionButtonUI : Control
    {
        [Export] private Array<DirectionSelectionButton> buttons;

        public override void _Ready()
        {
            
        }

        public void RevealButtons(GridSpace space, Dictionary<CharacterDirection, Array<GridSpace>> targets)
        {
            Position = space.GlobalPosition;

            foreach (var button in buttons)
            {
                button.RevealButton(targets);
            }
        }

        public void HideButtons()
        {
            foreach(var button in buttons)
            {
                button.HideButton();
            }
        }
    }
}