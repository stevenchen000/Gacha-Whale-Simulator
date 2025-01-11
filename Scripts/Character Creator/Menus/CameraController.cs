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


        public void ResetCamera()
        {
            Position = new Vector2(0,0);
            Zoom = new Vector2(1,1);
        }

        public void Pan(Vector2 direction)
        {
            Position -= direction / Zoom.X;
        }

        public void ZoomCamera(float amount)
        {
            float currZoom = Zoom.X;
            float newZoom = currZoom + amount / 1000 * zoomSpeed * currZoom;
            newZoom = Mathf.Max(newZoom, 0);

            Vector2 newZoomVec = new Vector2(newZoom, newZoom);
            Zoom = newZoomVec;
        }

        public void CenterAroundArea(Vector2 startPos, Vector2 endPos)
        {
            float size = (endPos - startPos).X * (1/centeredBorderPercent);
            
            if (size == 0) return;

            var center = (startPos + endPos) / 2;
            float screenWidth = DisplayServer.ScreenGetSize().X;

            float zoomSize = screenWidth / size;
            Zoom = new Vector2(zoomSize, zoomSize);
            Position = center;
        }
    }
}