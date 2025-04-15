using CombatSystem;
using Godot;
using System;

namespace PartyMenuSystem {
    public partial class PartyCharacterDisplay : Node
    {
        private CharacterData character;
        private SimpleWeakRef<PartyMenu> menu;

        [Export] private int index = 0;
        [Export] private CharacterDetailedDisplay charDisplay;


        public override void _Ready()
        {
            var tempMenu = Utils.FindParentOfType<PartyMenu>(this);
            menu = new SimpleWeakRef<PartyMenu>(tempMenu);
        }

        public void SetupDisplay(CharacterData character)
        {
            this.character = character;
            UpdateDisplay();
        }

        public void OpenCharacterSelector()
        {
            menu.Value.OpenCharacterSelector();
            menu.Value.SetPartyMemberIndex(index);
        }


        private void UpdateDisplay()
        {
            charDisplay.Init(character);
        }
    }
}