using Godot;
using System;

[Tool]
public partial class PortraitEditorTool : Sprite2D
{
    [Export] private CharacterPortrait portrait;
    [Export] private Control previewBox;
    private CharacterPortrait prevPortrait;
    private Texture2D prevTexture = null;
    private Vector2 prevPosition;
    private Vector2 prevScale;

    [Export] private Sprite2D previewSprite;

    public override void _Ready()
    {
        base._Ready();
        GD.Print("Portrait Editor active");
    }

    public override void _Process(double delta)
    {
        if (Engine.IsEditorHint())
        {
            if(portrait != prevPortrait)
            {
                LoadNewPortrait();
            }
            else if (PortraitDifferent())
            {
                GD.Print("Texture changed");
                ReplaceTexture();
                prevTexture = portrait.GetPortrait();
            }
            else if(DataChanged())
            {
                UpdateResource();
                UpdatePreviewSprite();
            }
        }
    }

    private void LoadNewPortrait()
    {
        if (prevPortrait != null)
        {
            ResourceSaver.Save(prevPortrait);
        }
        prevPortrait = portrait;
        ReplaceTexture();
        prevTexture = portrait.GetPortrait();
    }

    private void ReplaceTexture()
    {
        Texture = GetPortraitTexture();

        var pos = portrait.Position;
        var scale = portrait.Scale;

        Position = pos;
        Scale = new Vector2(scale, scale);
        

        if(previewSprite != null)
        {
            previewSprite.Texture = GetPortraitTexture();
        }
    }

    private void UpdateResource()
    {
        if(portrait != null)
        {
            portrait.SetPosition(Position);
            portrait.SetScale(Scale);
        }
    }

    private void UpdatePreviewSprite()
    {
        if(previewSprite != null)
        {
            var pos = Position;
            var scale = Scale;

            previewSprite.Position = pos;
            previewSprite.Scale = scale;
            portrait.SetupPortraitSimple(previewBox, previewSprite);
        }
    }



    public bool PortraitDifferent()
    {
        Texture2D portraitTexture = GetPortraitTexture();
        //Texture2D spriteTexture = GetSpriteTexture();

        return portraitTexture != prevTexture;
    }

    private Texture2D GetPortraitTexture()
    {
        Texture2D portraitTexture = null;

        if (portrait != null)
        {
            portraitTexture = portrait.GetPortrait();
        }

        return portraitTexture;
    }

    private bool DataChanged()
    {
        return  prevTexture != portrait.GetPortrait() ||
                prevPosition != Position ||
                prevScale != Scale;
    }

    private void UpdatePreviousData()
    {
        prevTexture = portrait.GetPortrait();
        prevPosition = Position;
        prevScale = Scale;
    }

    public Texture2D GetSpriteTexture()
    {
        Texture2D spriteTexture = null;

        if (spriteTexture != null)
        {
            spriteTexture = Texture;
        }

        return spriteTexture;
    }
}
