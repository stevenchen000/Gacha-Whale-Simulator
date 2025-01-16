using System;
using Godot;

namespace CharacterCreator
{
    public partial class CharacterCreatorPreview : TextureRect
    {
        [Export] private CharacterPortraitDisplay display;
        [Export] private PortraitBorder portraitBorder;

        public override void _Ready()
        {
            display.SetBorder(portraitBorder);
        }

        public override void _Process(double delta)
        {
            
        }

        public void RemovePortrait()
        {
            
        }

        public void UpdatePortrait(CustomCharacterPortrait portrait)
        {
            display.UpdatePortrait(portrait);
        }
    }
}
