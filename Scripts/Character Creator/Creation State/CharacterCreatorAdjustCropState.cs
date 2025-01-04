using System;
using Godot;

namespace CharacterCreator
{
    public partial class CharacterCreatorAdjustCropState : CharacterCreatorState
    {
        [Export] private Sprite2D spriteToEdit;
        [Export] private TextureRect cropBox;
        [Export] private CharacterCreatorPreview previewBox;
        [Export] private Camera2D cam;
        [Export] private float zoomSpeed = 1f;
        private Vector2 prevTouchPosition;
        private float pinchDistance = 0;
        private bool isTouching = false;
        private bool touchingCropBox = false;
        private bool isPinching = false;

        protected override void OnStateActivated()
        {
            isTouching = false;
        }

        protected override void RunState(double delta)
        {
            var touches = cropper.touches;

            if(touches.ContainsKey(0) && touches.ContainsKey(1))
            {
                ZoomAndPan();
                isTouching = false;
            }
            else if (touches.ContainsKey(0))
            {
                var newPos = touches[0];

                if (!isTouching)
                {
                    prevTouchPosition = newPos;
                    isTouching = true;
                    touchingCropBox = TouchIsInsideCropBox(newPos);
                }else if (touchingCropBox)
                {
                    DragCropBox();
                }
                else
                {
                    PanCamera();
                }
                isPinching = false;
            }
            else
            {
                isTouching = false;
                isPinching = false;
            }
            UpdatePreview();
        }

        private void UpdatePreview()
        {
            var texture = spriteToEdit.Texture;
            var startPos = cropper.cropStart;
            var endPos = cropper.cropEnd;
            float size = (endPos - startPos).X;
            previewBox.UpdatePortrait(texture, startPos, size);
        }

        private void ZoomAndPan()
        {
            var touches = cropper.touches;
            var touch1Pos = touches[0];
            var touch2Pos = touches[1];
            float distance = (touch1Pos - touch2Pos).Length();
            var center = (touch1Pos + touch2Pos) / 2;

            if (!isPinching)
            {
                isPinching = true;
                pinchDistance = distance;
                prevTouchPosition = center;
            }
            else
            {
                float diff = UpdatePinchAndReturnDifference(distance);
                _ZoomCamera(diff);

                var centerDiff = UpdateTouchPositionAndReturnDifference(center);
                _PanCamera(centerDiff);
            }

        }

        private void _ZoomCamera(float zoomAmount)
        {
            float currZoom = cam.Zoom.X;
            float newZoom = currZoom + zoomAmount / 1000 * zoomSpeed * currZoom;
            newZoom = Mathf.Max(newZoom, 0);

            Vector2 newZoomVec = new Vector2(newZoom, newZoom);
            cam.Zoom = newZoomVec;
        }

        private void PanCamera()
        {
            var touches = cropper.touches;
            var newPos = touches[0];

            var diff = UpdateTouchPositionAndReturnDifference(newPos);
            _PanCamera(diff);
        }

        private void _PanCamera(Vector2 distance)
        {
            cam.Position -= distance;
        }

        private void DragCropBox()
        {
            var touches = cropper.touches;
            var newPos = touches[0];
            cropper.MoveCropAreaToPosition(newPos);
        }

        protected override void OnStateDeactivated()
        {

        }


        /*****************
         * Helpers
         * ****************/


        private Vector2 UpdateTouchPositionAndReturnDifference(Vector2 newPos)
        {
            var diff = newPos - prevTouchPosition;
            diff /= cam.Zoom.X;
            prevTouchPosition = newPos;
            return diff;
        }

        private float UpdatePinchAndReturnDifference(float newDistance)
        {
            float diff = newDistance - pinchDistance;
            pinchDistance = newDistance;

            return diff;
        }

        private bool TouchIsInsideCropBox(Vector2 touchPosition)
        {
            var newPos = cropper.PositionToCameraPosition(touchPosition);
            var startPos = cropBox.GlobalPosition;
            var stopPos = startPos + cropBox.Size;


            return _TouchIsBetweenCorners(newPos, startPos, stopPos);
        }

        private bool _TouchIsBetweenCorners(Vector2 pos, Vector2 startCorner, Vector2 stopCorner)
        {
            bool xIsBetween = pos.X >= startCorner.X && pos.X <= stopCorner.X;
            bool yIsBetween = pos.Y >= startCorner.Y && pos.Y <= stopCorner.Y;

            return xIsBetween && yIsBetween;
        }
    }
}
