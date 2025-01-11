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
        private bool triedToZoom = false;

        protected override void OnStateActivated()
        {
            cropper.ActivateCropBox();
        }

        protected override void RunState(double delta)
        {
            var touches = cropper.touches.touchPositions;

            //reset crop if trying to zoom in
            if(touches.ContainsKey(0) && touches.ContainsKey(1))
            {
                Zoom();
            }//crop, but ignore if tried to zoom until both fingers are lifted
            else if (touches.ContainsKey(0) && !triedToZoom)
            {
                Crop();
            }
            else
            {
                float cropSize = cropper.GetCropSize();
                if (startedCrop)
                {
                    if (cropSize > cropper.MinCropSize)
                    {
                        ChangeState(adjustState);
                    }
                    else
                    {
                        cropper.Reset();
                    }
                }
                //cancel zoom status once both fingers lifted
                if (triedToZoom && !touches.ContainsKey(0) && !touches.ContainsKey(1))
                {
                    //GodotHelper.Print(this, "Canceled zoom");
                    triedToZoom = false;
                }
            }
        }

        private void Zoom()
        {
            if (startedCrop)
            {
                cropper.Reset();
                startedCrop = false;
                triedToZoom = true;
            }
            var pinchDelta = cropper.touches.pinchDelta;
            var pinchMovement = cropper.touches.pinchMovement;
            var cam = cropper.cam;
            cam.ZoomCamera(pinchDelta);
            cam.Pan(pinchMovement);
            //GodotHelper.Print(this, "Zooming");
        }

        private void Crop()
        {
            var touches = cropper.touches.touchPositions;
            var position = touches[0];

            if (!startedCrop)
            {
                cropper.SetCropAreaStart(position);
                startedCrop = true;
            }
            else
            {
                cropper.UpdateCropAreaEnd(position);
            }
            //GodotHelper.Print(this, "Cropping");
        }

        protected override void OnStateDeactivated()
        {
            cropper.CenterCamera();
            startedCrop = false;
        }
    }
}
