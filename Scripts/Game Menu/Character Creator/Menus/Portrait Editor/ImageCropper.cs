using Godot;
using System;
using Godot.Collections;

namespace CharacterCreator
{
    public partial class ImageCropper : Node
    {
        [Export] private Sprite2D spriteToEdit;
        [Export] private Sprite2D secondSprite;
        [Export] private TextureRect cropBox;
        [Export] private TextureRect cropCover;
        [Export] private Sprite2D cropStartCorner;
        [Export] private Sprite2D cropEndCorner;
        [Export] public float MinCropSize { get; private set; } = 10f;
        [Export] public CameraController cam { get; private set; }
        private CustomCharacterPortrait portraitToEdit;
        [Export] private CharacterPortraitDisplay preview;

        public bool Enabled { get; private set; }

        public Vector2 cropStart { get; set; }
        public Vector2 cropEnd { get; set; }

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


        }

        #endregion

        public void SetPortraitToEdit(CustomCharacterPortrait portrait)
        {
            portraitToEdit = portrait;
            preview.UpdatePortrait(portrait);
            var texture = portrait.GetPortrait();
            spriteToEdit.Texture = texture;
            secondSprite.Texture = texture;

            var cropStart = portrait.startArea;
            if (cropStart != Vector2.Zero)
            {
                float size = portrait.size;
                var offset = new Vector2(size, size);
                var cropEnd = cropStart + offset;
                SetupCropArea(cropStart, cropEnd);
                CenterCamera();
            }
            else
            {
                cam.ResetCamera();
            }

            ActivateCropBox();
            Enabled = true;
        }

        public void Disable()
        {
            Enabled = false;
            Reset();
        }

        public void UpdatePreview()
        {
            portraitToEdit.startArea = cropStart;
            portraitToEdit.size = GetCropSize();
            preview.UpdatePortrait(portraitToEdit);
        }

        public void Reset()
        {
            cropStart = new Vector2(0, 0);
            cropEnd = new Vector2(0, 0);
            ResetCropArea();
            cam.ResetCamera();
        }

        public void CenterCamera()
        {
            if(cropStart == cropEnd)
            {
                var texture = spriteToEdit.Texture;
                float width = texture.GetWidth();
                float height = texture.GetHeight();
                var tempStart = new Vector2(-width / 2, -height / 2);
                var tempEnd = -tempStart;
                cam.CenterAroundArea(tempStart, tempEnd);
            }
            else
            {
                cam.CenterAroundArea(cropStart, cropEnd);
            }
        }

        
        /**************
         * Crop Box
         * ************/

        #region crop box

        public void ActivateCropBox()
        {
            cropCover.Visible = true;
            cropBox.Visible = true;
        }

        public void DeactivateCropBox()
        {
            cropCover.Visible = false;
            cropBox.Visible = false;
        }

        public void SetupCropArea(Vector2 startPos, Vector2 endPos)
        {
            cropStart = startPos;
            cropEnd = endPos;
            cropBox.Position = startPos;
            float size = (endPos - startPos).X;
            var cropSize = new Vector2(size, size);
            cropBox.Size = cropSize;
            MoveSecondSpriteToSprite();
        }

        public void SetCropAreaEnd(Vector2 endPos)
        {
            var newPos = PositionToCameraPosition(endPos);
            var tempSize = (newPos - cropStart).X;
            tempSize = MathF.Max(MinCropSize, tempSize);
            var newSize = new Vector2(tempSize, tempSize);
            cropBox.Size = newSize;
            cropEnd = cropStart + newSize;
        }

        public void SetCropAreaStart(Vector2 startPos)
        {
            var newPos = PositionToCameraPosition(startPos);
            cropStart = newPos;
            cropBox.GlobalPosition = newPos;
            MoveSecondSpriteToSprite();
        }

        public void UpdateCropAreaStart(Vector2 startPos)
        {
            var newPos = PositionToCameraPosition(startPos);
            var xDiff = (cropEnd - newPos).X;
            xDiff = MathF.Max(xDiff, MinCropSize);
            var cropSize = new Vector2(xDiff, xDiff);
            cropStart = cropEnd - cropSize;
            cropBox.GlobalPosition = cropStart;
            cropBox.Size = cropSize;
            MoveSecondSpriteToSprite();
        }

        public void UpdateCropAreaEnd(Vector2 endPos)
        {
            var newPos = PositionToCameraPosition(endPos);
            var xDiff = (newPos - cropStart).X;
            xDiff = MathF.Max(xDiff , MinCropSize);
            var cropSize = new Vector2(xDiff, xDiff);
            cropEnd = cropStart + cropSize;
            cropBox.Size = cropSize;
        }

        public void MoveCropArea(Vector2 moveVector)
        {
            cropStart += moveVector;
            cropEnd += moveVector;
            cropBox.GlobalPosition = cropStart;
            MoveSecondSpriteToSprite();
        }

        public void MoveCropAreaToPosition(Vector2 newPosition)
        {
            var newPos = PositionToCameraPosition(newPosition);
            var size = cropEnd - cropStart;
            cropStart = newPos - size / 2;
            cropEnd = newPos + size / 2;
            cropBox.GlobalPosition = cropStart;
            cropBox.Position = cropBox.Position;
            MoveSecondSpriteToSprite();
        }

        public Vector2 PositionToCameraPosition(Vector2 position)
        {
            var camTransform = cam.GetViewport().CanvasTransform;
            var inverse = camTransform.AffineInverse();
            return inverse * position;
        }

        private void MoveSecondSpriteToSprite()
        {
            secondSprite.GlobalPosition = spriteToEdit.GlobalPosition;
        }

        public void ResetCropArea()
        {
            cropBox.Size = new Vector2(0, 0);
        }

        #endregion

        /*******************
         * Crop Image
         * ****************/

        #region crop image


        private void CropStart(Vector2 startPosition)
        {
            cropStart = startPosition;
            cropBox.GlobalPosition = startPosition;
        }


        #endregion


        /***************
         * Zoom and Pan
         * *************/

        #region Zoom



        #endregion

        /***************
         * Getters/Setters
         * ***************/

        #region Getters/Setters

        public float GetCropSize()
        {
            return Math.Abs((cropEnd - cropStart).X);
        }

        #endregion
    }
}