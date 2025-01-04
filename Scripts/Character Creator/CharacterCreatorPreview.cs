using System;
using Godot;

namespace CharacterCreator
{
    public partial class CharacterCreatorPreview : TextureRect
    {
        //[Export] private TextureRect previewBox;
        [Export] private Sprite2D characterPortrait;

        public override void _Process(double delta)
        {
            if(characterPortrait.Texture != null)
            {
                Visible = true;
            }
            else
            {
                Visible = false;
            }
        }

        public void RemovePortrait()
        {
            characterPortrait.Texture = null;
        }

        public void UpdatePortrait(Texture2D portrait, Vector2 startPos, float size)
        {
            characterPortrait.Texture = portrait;
            float previewSize = Size.X;
            float sizeScale = previewSize / size;
            characterPortrait.Scale = new Vector2(sizeScale, sizeScale);
            var newPos = ConvertUIPosToSpritePos(startPos, size) * sizeScale;
            var halfBoxSize = Size / 2;

            characterPortrait.Position = halfBoxSize - newPos;
            //first scale portrait so that the cropped area matches the new area
            //convert startPos and size into an offset
                //position + half of size to shift anchor of UI element to center
                //multiply result by size scale from earlier
            //Take half the size of the new box to show the image in and subtract the
                //converted position to get the new offset
            //scale of the box is irrelevant, so easy to manage
        }

        private Vector2 ConvertUIPosToSpritePos(Vector2 position, float size)
        {
            var cropBoxOffset = new Vector2(size / 2, size / 2);
            return (position + cropBoxOffset);
        }
    }
}
