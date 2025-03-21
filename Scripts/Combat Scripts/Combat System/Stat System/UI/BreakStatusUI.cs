using Godot;
using System;

namespace CombatSystem
{
    public partial class BreakStatusUI : Node
    {
        [Export] private Control node;
        [Export] private Color defaultColor;
        [Export] private Color breakColor;
        [Export] private BattleCharacter character;

        public void UpdateStatus()
        {
            Utils.Print(this, "Updating break UI...");
            Utils.Print(this, $"Character is broken: {character.IsBroken()}");
            if (character.IsBroken())
            {
                node.Modulate = breakColor;
            }
            else
            {
                node.Modulate = defaultColor;
            }
        }
    }
}