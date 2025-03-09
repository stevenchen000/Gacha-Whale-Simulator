using Godot;
using System;
using Godot.Collections;
using CharacterCreator;

namespace PartyMenuSystem
{
    public partial class CurrentPartySubmenu : Control
    {
        private SimpleWeakRef<PartyMenu> menu;
        [Export] private Control partyButtonContainer;
        private Array<PartyCharacterDisplay> characterDisplays;

        public override void _Ready()
        {
            base._Ready();

            CallDeferred(MethodName._Init);
            CallDeferred(MethodName.Enable);
        }

        public void _Init()
        {
            var tempMenu = Utils.FindParentOfType<PartyMenu>(this);
            menu = new SimpleWeakRef<PartyMenu>(tempMenu);

            FindDisplays();
            SetupDisplays();
        }


        public void Disable()
        {
            Visible = false;
            Position = new Vector2(9999, 9999);
        }

        public void Enable()
        {
            Visible = true;
            Position = new Vector2(0, 0);
            SetupDisplays();
        }



        /************************
         * Setup
         * *******************/

        private PartySetup GetCurrentParty()
        {
            return menu.Value.CurrentParty;
        }

        private void FindDisplays()
        {
            var children = partyButtonContainer.GetChildren();
            characterDisplays = new Array<PartyCharacterDisplay>();

            foreach (var child in children)
            {
                if(child is PartyCharacterDisplay)
                {
                    var display = (PartyCharacterDisplay)child;
                    characterDisplays.Add(display);
                }
            }
        }

        private void SetupDisplays()
        {
            var party = GetCurrentParty();
            var partyMembers = party.Party;

            for (int i = 0; i < characterDisplays.Count; i++)
            {
                var display = characterDisplays[i];
                var member = partyMembers[i];

                display.SetupDisplay(member);
            }
        }
    }
}