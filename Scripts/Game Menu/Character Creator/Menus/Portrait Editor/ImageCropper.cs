using Godot;
using System;
using Godot.Collections;

namespace CharacterCreator
{
    public partial class ImageCropper : Control
    {
        [Export] private Sprite2D spriteToEdit;
        [Export] private Sprite2D secondSprite;
        [Export] private TextureRect cropCover;


        private CustomCharacterPortrait portraitToEdit;
        [Export] private CharacterPortraitDisplay preview;

        [ExportCategory("Cropping Settings")]
        [Export] private float scaleSpeed = 100f;

        /**********************
        * Main Functions
        * *******************/

        #region Main Functions

        public override void _Ready()
        {
            //touches = new Dictionary<int, Vector2>();
            cropCover.Visible = false;
        }


        public override void _Process(double delta)
        {
            HandleMovement();
        }

        #endregion


        private void HandleMovement()
        {
            int touches = TouchHandler.NumOfTouches;

            if(touches == 1)
            {
                HandlePan();
            }else if(touches >= 2)
            {
                HandlePanAndScale();
            }
        }

        private void HandlePan()
        {
            if (TouchHandler.TouchPositions.ContainsKey(0))
            {
                var distance = TouchHandler.DragDistance[0];
                PanSprite(distance);
            }
        }

        private void HandlePanAndScale()
        {
            float pinchDelta = TouchHandler.PinchDelta;
            Vector2 pinchMovement = TouchHandler.PinchMovement;

            PanSprite(pinchMovement);
            ScaleSprite(pinchDelta);
        }


        /*****************
         * Sprite Movement
         * **************/

        public void PanSprite(Vector2 panAmount)
        {
            var currTransform = spriteToEdit.GlobalPosition;
            currTransform += panAmount;

            spriteToEdit.GlobalPosition = currTransform;
            secondSprite.GlobalPosition = currTransform;
        }

        public void ScaleSprite(float scaleAmount)
        {
            float currScale = spriteToEdit.Scale.X;
            float newScale = currScale + scaleAmount / 1000 * scaleSpeed * currScale;

            var newSpriteScale = new Vector2(newScale, newScale);
            spriteToEdit.Scale = newSpriteScale;
            secondSprite.Scale = newSpriteScale;
        }

        public void SetSpriteTilt(float rotation)
        {
            spriteToEdit.Rotation = rotation;
        }









        public void SetPortraitToEdit(CustomCharacterPortrait portrait)
        {
            var texture = portrait.GetPortrait();
            spriteToEdit.Texture = texture;
            secondSprite.Texture = texture;

            var position = portrait.Position;
            if(position == Vector2.Zero)
            {
                ResetPosition();
            }
        }

        public void Disable()
        {
            
        }

        public void UpdatePreview()
        {
            
        }

        public void ResetPosition()
        {
            var texture = spriteToEdit.Texture;
            var textureSize = texture.GetSize();
            float textureX = textureSize.X;
            float textureY = textureSize.Y;

            var textureAspect = textureSize.X / textureSize.Y;

            float scale = 1;

            if (textureAspect > 9.0 / 16.0)
                scale = 900 / textureX;
            else
                scale = 1700 / textureY;

            SetSpriteScale(scale);
        }

        public void SetSpriteScale(float scale)
        {
            var newScale = new Vector2(scale, scale);

            spriteToEdit.Scale = newScale;
            secondSprite.Scale = newScale;
        }

        /*****************
         * Getters / Setters
         * ****************/

        public Vector2 GetSpriteOffset()
        {
            return spriteToEdit.Position;
        }

        public float GetSpriteScale()
        {
            return spriteToEdit.Scale.X;
        }
    }
}