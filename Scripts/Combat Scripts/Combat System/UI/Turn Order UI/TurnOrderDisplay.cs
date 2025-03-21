using Godot;
using Godot.Collections;
using System;

namespace CombatSystem
{
    public partial class TurnOrderDisplay : Node
    {
        [Export] private Node container;
        [Export] private Array<TurnOrderPortrait> displays = new Array<TurnOrderPortrait>();

        private BattleManager battle;

        public override void _Ready()
        {
            battle = Utils.FindParentOfType<BattleManager>(this);
        }

        public void UpdateDisplay()
        {
            var turns = battle.State.TurnOrder.turnOrder;

            for(int i = 0; i < displays.Count; i++)
            {
                var display = displays[i];
                var character = turns[i].Character;
                display.UpdatePortrait(character);
            }
        }
    }
}