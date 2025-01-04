using System;
using Godot;
using StateSystem;

namespace CharacterCreator
{
    public partial class CharacterCreatorState : StateNode
    {
        protected ImageCropper cropper;

        public override void _Ready()
        {
            base._Ready();
            cropper = (ImageCropper)FindParent("Image Cropper");
        }

        protected override void OnStateActivated()
        {

        }

        protected override void RunState(double delta)
        {

        }

        protected override void OnStateDeactivated()
        {

        }
    }
}
