using Godot;
using System;
using System.IO;

public partial class ImageSaver : Node
{
    private string path;

    public override void _Ready()
    {
        path = ProjectSettings.GlobalizePath("user://Images");
        CreatePathIfDoesNotExist("user://Images");
        CreatePathIfDoesNotExist("user://Portraits");
        CreatePathIfDoesNotExist("user://Characters");
    }

    private void CreatePathIfDoesNotExist(string relativePath)
    {
        string path = ProjectSettings.GlobalizePath(relativePath);
        if (!Directory.Exists(path))
        {
            DirAccess.MakeDirRecursiveAbsolute(path);
        }
    }

    public void SaveSpriteToFile(Node node)
    {
        if(node is Sprite2D)
        {
            var sprite = (Sprite2D)node;
            var texture = sprite.Texture;
            string filename = CreateFileName();
            var newTexture = CustomCharacterManager.SaveImage(texture);
            var portrait = new CustomCharacterPortrait(newTexture);
            CustomCharacterManager.SavePortrait(portrait);
            node.CallDeferred(MethodName.QueueFree);
        }
    }



    public void SaveImage()
    {
        
    }

    public void LoadImage()
    {

    }

    public string CreateFileName()
    {
        var now = System.DateTime.Now;
        var year = now.Year;
        var month = now.Month;
        var day = now.Day;
        var hour = now.Hour;
        var minute = now.Minute;
        var seconds = now.Second;
        var milliseconds = now.Millisecond;

        string result = $"{year}-{month}-{day}_{hour}-{minute}-{seconds}-{milliseconds}.png";
        
        if (File.Exists(result))
        {
            Utils.Print(this, "File already exists!");
        }

        return result;
    }
}
