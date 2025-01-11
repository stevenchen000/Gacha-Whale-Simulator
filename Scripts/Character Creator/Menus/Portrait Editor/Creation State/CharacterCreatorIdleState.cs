using System;
using Godot;

namespace CharacterCreator
{
    public partial class CharacterCreatorIdleState : CharacterCreatorState
    {
        [Export] private CharacterCreatorCropState cropState;

        protected override void OnStateActivated()
        {
            GodotHelper.Print(this, "Character cropper is idle");
            cropper.Reset();
        }

        protected override void RunState(double delta)
        {
            GodotHelper.Print(this, "Running idle state");
            if (cropper.Enabled)
            {
                GodotHelper.Print(this, "Not idling anymore!");
                ChangeState(cropState);
            }
        }

        protected override void OnStateDeactivated()
        {

        }
    }
}
