using CombatSystem;
using Godot;
using System;

namespace PartyMenuSystem {
    public partial class PartyCharacterDisplay : Node
    {
        private CharacterData character;
        private SimpleWeakRef<PartyMenu> menu;

        [Export] private int index = 0;
        [Export] private CharacterPortraitDisplay display;
        [Export] private Label nameLabel;
        [Export] private Label levelLabel;


        public override void _Ready()
        {
            var tempMenu = Utils.FindParentOfType<PartyMenu>(this);
            menu = new SimpleWeakRef<PartyMenu>(tempMenu);
        }

        public void SetupDisplay(CharacterData character)
        {
            this.character = character;

            UpdatePortrait(character);
            UpdateName(character);
            UpdateLevel(character);
        }

        public void OpenCharacterSelector()
        {
            Utils.Print(this, "Pressed party button");
            menu.Value.OpenCharacterSelector();
            menu.Value.SetPartyMemberIndex(index);
        }


        private void UpdatePortrait(CharacterData character)
        {
            if (character != null)
            {
                var portrait = character.GetPortrait();

                display.UpdatePortrait(portrait);
            }
            else
            {
                display.UpdatePortrait(null);
            }
        }

        private void UpdateName(CharacterData data)
        {
            string name = "";

            if(data != null)
                name = data.Character.Name;

            nameLabel.Text = name;
        }

        private void UpdateLevel(CharacterData data)
        {
            string level = "";

            if(data != null)
            {
                int currLevel = data.Level;
                int maxLevel = data.LevelCap;
                level = $"Lv {currLevel}/{maxLevel}";
            }

            levelLabel.Text = level;
        }
    }
}