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
    [Export] private bool save = false;

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
                ReplaceTexture();
                prevTexture = portrait.GetPortrait();
            }else if(Texture != prevTexture && Texture != null)
            {
                portrait.SetPortrait(Texture);
            }
            else if (DataChanged())
            {
                UpdateResource();
                UpdatePreviewSprite();
            }

            if (save)
            {
                save = false;
                SaveFile(portrait);
            }
        }
    }

    private void LoadNewPortrait()
    {
        if (prevPortrait != null)
        {
            SaveFile(prevPortrait);
        }

        prevPortrait = portrait;

        if (portrait != null)
        {
            ReplaceTexture();
            prevTexture = portrait.GetPortrait();
        }
        else
        {
            prevTexture = null;
            portrait = new();
        }
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
        if(previewSprite != null && portrait != null)
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
        return  (portrait != null && prevTexture != portrait.GetPortrait()) ||
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

    /******************
     * Saver
     * **************/

    private string path = "res://Resources/Characters/Portraits/";

    private void SaveFile(CharacterPortrait portrait)
    {
        try
        {
            string filename = GetFilename(portrait);
            ResourceSaver.Save(portrait, filename);
            portrait.ResourcePath = filename;
            Utils.Print(this, filename);
        }
        catch (Exception ex)
        {
            Utils.Print(this, ex);
        }
    }

    private string GetFilename(CharacterPortrait portrait)
    {
        string filename = portrait.ResourcePath;
        if (filename == "")
        {
            string imagePath = portrait.GetPortrait().ResourcePath;
            string[] splitPath = imagePath.Split("/");
            int splitPathCount = splitPath.Length;
            string imageFilename = splitPath[splitPathCount - 1];
            string splitFilename = imageFilename.Split(".")[0];
            filename = $"{path}{splitFilename}.tres";
        }

        return filename;
    }
}
