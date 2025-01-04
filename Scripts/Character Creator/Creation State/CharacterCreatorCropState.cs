using System;
using Godot;
using Godot.Collections;

namespace CharacterCreator
{
    public partial class CharacterCreatorCropState : CharacterCreatorState
    {
        [Export] private Sprite2D spriteToEdit;
        [Export] private CharacterCreatorAdjustCropState adjustState;
        private bool startedCrop = false;

        protected override void OnStateActivated()
        {
            cropper.ActivateCropBox();
        }

        protected override void RunState(double delta)
        {
            var touches = cropper.touches;

            if (touches.ContainsKey(0))
            {
                var position = touches[0];

                if (!startedCrop)
                {
                    cropper.SetCropAreaStart(position);
                    startedCrop = true;
                }
                else
                {
                    cropper.SetCropAreaEnd(position);
                }
            }else
            {
                if (startedCrop)
                {
                    ChangeState(adjustState);
                }
            }
        }

        protected override void OnStateDeactivated()
        {

        }
    }
}
