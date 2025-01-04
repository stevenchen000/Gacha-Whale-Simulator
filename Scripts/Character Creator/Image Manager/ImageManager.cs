using Godot;
using System;
using System.IO;
using Godot.Collections;

public partial class ImageManager : Node
{
    private static ImageManager _instance;
    private Dictionary<string, ImageTexture> _textures;
    private static string imageSaveLocation = "user://Images";

    public override void _Ready()
    {
        if(_instance == null)
        {
            _instance = this;
            DirAccess.MakeDirRecursiveAbsolute(imageSaveLocation);
            _textures = new Dictionary<string, ImageTexture>();
        }
        GD.Print("Ready");
    }

    /// <summary>
    /// Filename is just the name of the file without the extension or path
    /// </summary>
    /// <param name="texture"></param>
    /// <param name="filename"></param>
    public void SaveImage(Texture2D texture, string filename)
    {
        string filepath = GetFilePath(filename);
        var image = texture.GetImage();
        image.SavePng(filepath);
        _textures[filename] = ImageTexture.CreateFromImage(image);
    }

    /// <summary>
    /// Filename is just the name of the file without the extension or path
    /// </summary>
    /// <param name="texture"></param>
    /// <param name="filename"></param>
    public void LoadImage(string filename)
    {
        string path = GetFilePath(filename);
        var image = Image.LoadFromFile(path);
        var texture = ImageTexture.CreateFromImage(image);
        _textures[filename] = texture;
        GD.Print("Loaded image");
    }

    public static string GetFilePath(string filename)
    {
        return $"{imageSaveLocation}//{filename}.png";
    }

    public static bool FileExists(string filename)
    {
        var filepath = GetFilePath(filename);
        return File.Exists(filepath);
    }
}
