using Godot;
using System;
using Godot.Collections;
using System.IO;

[GlobalClass]
[Tool]
public partial class CharacterPortrait : Resource
{
    [Export] private Texture2D portrait;
    [Export] private Dictionary<string, Texture2D> emotions = new Dictionary<string, Texture2D>();
    [Export] public Vector2 Position { get; private set; }
    [Export] public float Scale { get; private set; } = 1;

    [ExportCategory("CG Positions")]
    [Export] public Vector2 cgPosition { get; private set; }
    [Export] public float cgScale { get; private set; } = 1;

    public CharacterPortrait()
    {

    }


    /****************
     * Getters/Setters
    *****************/

    public virtual Texture2D GetPortrait(string emotion = "")
    {
        Texture2D result = portrait;

        if(emotions.ContainsKey(emotion))
            result = emotions[emotion];

        return result;
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
            float textureX = GetPortrait().GetSize().X;
            float textureY = GetPortrait().GetSize().Y;
            float maxSize = MathF.Max(textureX, textureY);
            float sizeScale = previewSize / maxSize;
            spriteElement.Position = box.Size / 2;
            spriteElement.Scale = new Vector2(sizeScale, sizeScale);
        }
        else
        {
            spriteElement.Texture = null;
        }
    }

}
