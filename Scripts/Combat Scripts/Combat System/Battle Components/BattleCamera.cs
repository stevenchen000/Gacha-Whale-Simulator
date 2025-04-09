using Godot;
using System;

namespace CombatSystem
{
    public partial class BattleCamera : Node2D
    {
        private Vector2 defaultPosition;
        private Vector2 defaultZoom;
        private Camera2D camera;

        private Vector2 targetPosition;
        private Vector2 targetZoom;

        public override void _Ready()
        {
            defaultPosition = Position;
            targetPosition = Position;
            camera = Utils.FindChildOfType<Camera2D>(this);
            targetZoom = Vector2.One;
            defaultZoom = targetZoom;
        }

        public override void _Process(double delta)
        {
            Position = Position.Lerp(targetPosition, 10f * (float)delta);
            camera.Zoom = camera.Zoom.Lerp(targetZoom, camera.Zoom.X * (float)delta);
        }

        public void ResetPosition()
        {
            targetPosition = defaultPosition;
        }

        public void MoveTowardsPosition(Vector2 newPosition)
        {
            targetPosition = newPosition;
        }

        public void SetZoom(Vector2 zoom)
        {
            camera.Zoom = zoom;
        }

        public void SetZoom(float zoom)
        {
            camera.Zoom = new Vector2(zoom, zoom);
        }
    }
}