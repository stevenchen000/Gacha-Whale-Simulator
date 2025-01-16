using Godot;
using System;
using Godot.Collections;
using EventSystem;

namespace CharacterCreator
{
    public partial class CameraController : Camera2D
    {
        [Export] private float zoomSpeed = 1f;
        [Export] private float centeredBorderPercent = 0.5f;

        private Vector2 moveToPos;
        private Vector2 moveToZoom;

        public override void _Process(double delta)
        {
            base._Process(delta);
            if(moveToPos != Vector2.Zero)
            {
                Position = Position.Lerp(moveToPos, 5 * (float)delta);
            }

            if (moveToZoom != Vector2.Zero)
            {
                Zoom = Zoom.Lerp(moveToZoom, 3 * (float)delta);
            }
        }



        public void ResetCamera()
        {
            Position = new Vector2(0,0);
            Zoom = new Vector2(1,1);
        }

        public void Pan(Vector2 direction)
        {
            Position -= direction / Zoom.X;
            moveToPos = Vector2.Zero;
        }

        public void ZoomCamera(float amount)
        {
            float currZoom = Zoom.X;
            float newZoom = currZoom + amount / 1000 * zoomSpeed * currZoom;
            newZoom = Mathf.Max(newZoom, 0);

            Vector2 newZoomVec = new Vector2(newZoom, newZoom);
            Zoom = newZoomVec;
            moveToZoom = Vector2.Zero;
        }

        public void CenterAroundArea(Vector2 startPos, Vector2 endPos)
        {
            float size = (endPos - startPos).X * (1/centeredBorderPercent);
            
            if (size == 0) return;

            var center = (startPos + endPos) / 2;
            float screenWidth = DisplayServer.ScreenGetSize().X;

            float zoomSize = screenWidth / size;
            moveToZoom = new Vector2(zoomSize, zoomSize);
            moveToPos = center;
        }
    }
}