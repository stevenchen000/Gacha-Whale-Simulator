using Godot;
using System;
using Godot.Collections;

[GlobalClass]
public partial class CharacterPortrait : Resource
{
    [Export] private Texture2D portrait;
    [Export] private string filename = "";
    [Export] private bool customFile = false;
    [Export] private Vector2 startArea;
    [Export] private float size = 0;

    public void SetupPortrait(Control box, Sprite2D spriteElement, float borderSize = 0)
    {
        var size = box.Size;

    }

    public void UpdatePortrait(Control box, Sprite2D spriteElement, float borderSize = 0)
    {
        if(portrait == null)
        {
            //Try to load portrait file
            LoadPortrait();
        }

        if (portrait != null)
        {
            float previewSize = box.Size.X - borderSize * 2;
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
    }

    private Vector2 ConvertUIPosToSpritePos(Control box, float sizeScale, float borderSize)
    {
        var cropBoxOffset = new Vector2(size / 2, size / 2);
        var boxSize = box.Size;
        return boxSize / 2 - (startArea + cropBoxOffset) * sizeScale;
    }

    private void LoadPortrait()
    {
        GD.Print("LoadPortrait() in CharacterPortrait.cs has not been implemented");
    }
}
