using System;
using Godot;

namespace CharacterCreator
{
    public partial class CharacterCreatorIdleState : CharacterCreatorState
    {
        [Export] private Sprite2D spriteToEdit;
        [Export] private CharacterCreatorCropState cropState;

        protected override void OnStateActivated()
        {
            GD.Print("Character cropper is idle");
        }

        protected override void RunState(double delta)
        {
            if (TextureLoaded())
            {
                GD.Print("A texture was loaded!");
                ChangeState(cropState);
            }
        }

        protected override void OnStateDeactivated()
        {

        }

        private bool TextureLoaded()
        {
            Texture2D texture = null;

            if(spriteToEdit != null)
            {
                texture = spriteToEdit.Texture;
            }

            return texture != null;
        }
    }
}
