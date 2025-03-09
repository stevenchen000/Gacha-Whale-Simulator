using Godot;
using System;
using Godot.Collections;
using System.IO;

[GlobalClass]
[Tool]
public partial class CharacterPortrait : Resource
{
    [Export] private Texture2D portrait;
    [Export] public Vector2 Position { get; private set; }
    [Export] public float Scale { get; private set; } = 1;
    [Export] public Vector2 startArea;
    [Export] public float size = 0;

    public CharacterPortrait()
    {

    }


    /****************
     * Getters/Setters
    *****************/

    public virtual Texture2D GetPortrait()
    {
        return portrait;
    }

    public void SetupPortraitSimple(Control box, Sprite2D spriteElement, float borderSize = 0)
    {
        spriteElement.Texture = GetPortrait();
        var boxSize = box.Size.X - borderSize*2;
        var boxCenterOffset = box.Size / 2;
        int baseSize = 100;
        var boxScale = boxSize / baseSize;

        var scale = Scale * boxScale;
        spriteElement.Scale = new Vector2(scale, scale);
        spriteElement.Position = Position * boxScale + boxCenterOffset;
    }

    public void SetPosition(Vector2 newPos) { Position = newPos; }
    public void SetScale(Vector2 scale) { Scale = scale.X; }






    public void SetupPortrait(Control box, Sprite2D spriteElement, float borderSize = 0)
    {
        spriteElement.Texture = GetPortrait();
        UpdatePortrait(box, spriteElement, borderSize);
    }

    public void UpdatePortrait(Control box, Sprite2D spriteElement, float borderSize = 0)
    {
        if (GetPortrait() != null)
        {
            float previewSize = box.Size.X - borderSize * 2;

            if (size != 0)
            {
                float sizeScale = previewSize / size;
                spriteElement.Scale = new Vector2(sizeScale, sizeScale);
                var newPos = ConvertUIPosToSpritePos(box, sizeScale, borderSize);

                spriteElement.Position = newPos;
                //first scale portrait so that the cropped area matches the new area
                //convert startPos and size into an offset
                //position + half of size to shift anchor of UI element to center
                //multiply result by size scale from earlier
                //Take half the size of the new box to show the image in and subtract the
                //converted position to get the new offset
                //scale of the box is irrelevant, so easy to manage
            }
            else
            {
                float textureX = GetPortrait().GetSize().X;
                float textureY = GetPortrait().GetSize().Y;
                float maxSize = MathF.Max(textureX, textureY);
                float sizeScale = previewSize / maxSize;
                spriteElement.Position = box.Size / 2;
                spriteElement.Scale = new Vector2(sizeScale, sizeScale);
            }
        }
        else
        {
            spriteElement.Texture = null;
        }
    }

    private Vector2 ConvertUIPosToSpritePos(Control box, float sizeScale, float borderSize)
    {
        var cropBoxOffset = new Vector2(size / 2, size / 2);
        var boxSize = box.Size;
        return boxSize / 2 - (startArea + cropBoxOffset) * sizeScale;
    }

}
