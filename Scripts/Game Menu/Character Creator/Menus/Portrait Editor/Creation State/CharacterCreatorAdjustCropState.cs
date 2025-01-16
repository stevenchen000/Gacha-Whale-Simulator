using System;
using System.Security.Cryptography.X509Certificates;
using Godot;

namespace CharacterCreator
{
    public partial class CharacterCreatorAdjustCropState : CharacterCreatorState
    {
        [Export] private Sprite2D spriteToEdit;
        [Export] private TextureRect cropBox;
        [Export] private CharacterCreatorState idleState;
        [Export] private CharacterCreatorState cropState;

        private CropStateEnum state = CropStateEnum.None;

        /**********************
        * Main Functions
        * *******************/

        #region Main Functions

        protected override void OnStateActivated()
        {
            Utils.Print(this, "Adjust crop");
        }

        protected override void RunState(double delta)
        {
            #region State Changes
            if (!cropper.Enabled)
            {
                ChangeState(idleState);
                return;
            }

            if(cropper.GetCropSize() == 0)
            {
                ChangeState(cropState);
                return;
            }
            #endregion



            var touches = TouchHandler.touchPositions;

            
            if(touches.ContainsKey(0) && touches.ContainsKey(1))
            {
                //zooming, supercedes everything
                ZoomAndPan();
                state = CropStateEnum.Zooming;
            }else if (state != CropStateEnum.Zooming && touches.ContainsKey(0))
            {
                var touchPos = touches[0];
                if (state == CropStateEnum.None)
                {
                    if (TouchIsNearCropStart())
                    {
                        state = CropStateEnum.DraggingStartCorner;
                    }else if (TouchIsNearCropEnd())
                    {
                        state = CropStateEnum.DraggingEndCorner;
                    }
                    else if (TouchIsInsideCropBox())
                    {
                        //GodotHelper.Print(this, "Touching crop box");
                        state = CropStateEnum.MovingCropBox;
                    }
                }
                else if (state == CropStateEnum.MovingCropBox)
                {
                    //GodotHelper.Print(this, "Dragging crop box");
                    DragCropBox();
                }
                else if (state == CropStateEnum.DraggingStartCorner)
                {
                    cropper.UpdateCropAreaStart(touchPos);
                }else if(state == CropStateEnum.DraggingEndCorner)
                {
                    cropper.UpdateCropAreaEnd(touchPos);
                }
            }
            else
            {
                //Cancels zoom once both fingers lifted
                if(state == CropStateEnum.Zooming && !touches.ContainsKey(0) && !touches.ContainsKey(1))
                {
                    state = CropStateEnum.None;
                }else if (state != CropStateEnum.Zooming && !touches.ContainsKey(0))
                {
                    state = CropStateEnum.None;
                }
            }

            
            UpdatePreview();
            cropper.UpdatePreview();
        }

        protected override void OnStateDeactivated()
        {

        }

        #endregion

        /*************
         * Zoom & Pan
         * ***********/

        #region Zoom & Pan

        private void UpdatePreview()
        {
            var texture = spriteToEdit.Texture;
            var startPos = cropper.cropStart;
            var endPos = cropper.cropEnd;
            float size = (endPos - startPos).X;
        }

        private void ZoomAndPan()
        {
            float pinchDelta = TouchHandler.pinchDelta;
            var pinchMovement = TouchHandler.pinchMovement;
            var cam = cropper.cam;
            cam.ZoomCamera(pinchDelta);
            cam.Pan(pinchMovement);
        }
        

        private void DragCropBox()
        {
            var touchDelta = TouchHandler.dragDistance[0];

            //GodotHelper.Print(this, touchDelta);
            var cam = cropper.cam;
            var camZoom = cam.Zoom.X;
            var moveDelta = touchDelta / camZoom;
            //GodotHelper.Print(this, moveDelta);
            cropper.MoveCropArea(moveDelta);
        }

        #endregion

        /*****************
         * Helpers
         * ****************/

        private bool TouchIsInsideCropBox()
        {
            var touchPosition = TouchHandler.touchPositions[0];
            var newPos = cropper.PositionToCameraPosition(touchPosition);
            var startPos = cropper.cropStart;
            var stopPos = cropper.cropEnd;


            return _TouchIsBetweenCorners(newPos, startPos, stopPos);
        }

        private bool _TouchIsBetweenCorners(Vector2 pos, Vector2 startCorner, Vector2 stopCorner)
        {
            bool xIsBetween = pos.X >= startCorner.X && pos.X <= stopCorner.X;
            bool yIsBetween = pos.Y >= startCorner.Y && pos.Y <= stopCorner.Y;

            return xIsBetween && yIsBetween;
        }

        private bool TouchIsNearCropStart()
        {
            var touchPos = TouchHandler.touchPositions[0];
            //gets corner size adjusted for screen size
            var startPos = cropBox.GlobalPosition;
            return _TouchIsNearPosition(touchPos, startPos, 50);
        }

        private bool TouchIsNearCropEnd()
        {
            var touchPos = TouchHandler.touchPositions[0];
            //gets corner size adjusted for screen size
            var endPos = cropper.cropEnd;
            return _TouchIsNearPosition(touchPos, endPos, 50);
        }

        private bool _TouchIsNearPosition(Vector2 touchPos, Vector2 posToCheck, float leeway)
        {
            float cornerSize = leeway / cropper.cam.Zoom.X;
            var newTouchPos = cropper.PositionToCameraPosition(touchPos);
            var distance = (newTouchPos - posToCheck).Length();

            return distance <= cornerSize;
        }
    }
}
