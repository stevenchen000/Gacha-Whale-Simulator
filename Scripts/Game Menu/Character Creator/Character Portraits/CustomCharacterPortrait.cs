using Godot;
using System;
using Godot.Collections;
using System.IO;


public partial class CustomCharacterPortrait : CharacterPortrait
{
    [Export] public string portraitFile { get; private set; } = "";

    public CustomCharacterPortrait()
    {

    }

    public CustomCharacterPortrait(Texture2D texture)
    {
        portraitFile = texture.ResourcePath;
    }

    public override Texture2D GetPortrait(string emotion = "")
    {
        return CustomCharacterManager.GetCustomTexture(portraitFile);
    }




    public void Save()
    {
        string filename = Path.GetFileNameWithoutExtension(portraitFile);
        string path = ProjectSettings.GlobalizePath("user://Portraits");
        ResourcePath = $"{path}/{filename}.tres";
        ResourceSaver.Save(this);
    }
}
