using System;
using Godot;

namespace CharacterCreator
{
    public partial class CharacterCreatorIdleState : CharacterCreatorState
    {
        [Export] private CharacterCreatorCropState cropState;
        [Export] private CharacterCreatorAdjustCropState adjustCropState;

        protected override void OnStateActivated()
        {
            
        }

        protected override void RunState(double delta)
        {
            /*if (cropper.Enabled)
            {
                Utils.Print(this, cropper.cropStart);
                if (cropper.cropStart == Vector2.Zero)
                {
                    ChangeState(cropState);
                }
                else
                {
                    ChangeState(adjustCropState);
                }
            }*/
        }

        protected override void OnStateDeactivated()
        {

        }
    }
}
