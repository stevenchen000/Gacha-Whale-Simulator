using CombatSystem;
using Godot;
using System;

namespace PartyMenuSystem
{
    public partial class PartyMemberSelectionButton : Control
    {
        [Export] private CharacterPortraitDisplay display;


        private SimpleWeakRef<PartyMenu> menu;
        private SimpleWeakRef<SelectPartyMemberSubmenu> submenu;
        private CharacterData character;


        public override void _Ready()
        {
            var tempMenu = Utils.FindParentOfType<PartyMenu>(this);
            menu = new SimpleWeakRef<PartyMenu>(tempMenu);
            var tempSubmenu = Utils.FindParentOfType<SelectPartyMemberSubmenu>(this);
            submenu = new SimpleWeakRef<SelectPartyMemberSubmenu>(tempSubmenu);
        }


        public void SetupButton(CharacterData character)
        {
            this.character = character;
            var portrait = character.GetPortrait();

            display.UpdatePortrait(portrait);
        }

        public void EnableButton()
        {
            Visible = true;
        }

        public void DisableButton()
        {
            Visible = false;
        }


        public void SelectCharacter()
        {
            menu.Value.SetCurrentMember(character);
            menu.Value.OpenPartyEditor();
        }

    }
}