using Godot;
using System;
using System.IO;

public partial class ImageSaver : Node
{
    [Export] private Sprite2D spriteToEdit;
    [Export] private Sprite2D secondSprite;
    private string path;

    public override void _Ready()
    {
        GD.Print("Ready");
    }

    public void SaveImage()
    {
        path = "user://TestCharacter";
        DirAccess.MakeDirRecursiveAbsolute(path);
        GD.Print("Directory created");
        spriteToEdit.Texture.GetImage().SavePng(path + "//test.png");
        GD.Print("Saved image");
    }

    public void LoadImage()
    {
        path = "user://TestCharacter//test.png";
        var image = Image.LoadFromFile(path);
        var texture = ImageTexture.CreateFromImage(image);
        spriteToEdit.Texture = texture;
        secondSprite.Texture = texture;
        GD.Print("Loaded image");
    }

}
