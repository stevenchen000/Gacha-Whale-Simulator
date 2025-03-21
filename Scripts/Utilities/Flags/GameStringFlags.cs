using Godot;
using System;
using Godot.Collections;

public partial class GameStringFlags : Node
{
    private Dictionary<string, string> flags = new Dictionary<string, string>();

    public override void _Ready()
    {
        /*Load();

        if (flags == null) flags = new Dictionary<string, string>();*/
    }

    public override void _Notification(int what)
    {
        if (what == NotificationWMCloseRequest || what == NotificationApplicationPaused)
        {
            Save();
        }
    }



    public string GetFlag(string flag)
    {
        if (flags.ContainsKey(flag))
            return flags[flag];
        else
            return "";
    }

    public void SetFlag(string flag, string text)
    {
        flags[flag] = text;
    }

    public void RemoveFlag(string flag)
    {
        if (flags.ContainsKey(flag))
        {
            flags.Remove(flag);
        }
    }


    /************************
     * Save and Load
     * ********************/

    private string filename = "user://savetext.json";

    public void Save()
    {
        string json = Json.Stringify(flags);
        var file = FileAccess.Open(filename, FileAccess.ModeFlags.Write);

        file.StorePascalString(json);

        file.Close();
    }

    public void Load()
    {
        if (FileAccess.FileExists(filename))
        {
            var file = FileAccess.Open(filename, FileAccess.ModeFlags.Read);

            string json = file.GetPascalString();
            flags = (Dictionary<string, string>)Json.ParseString(json);

            file.Close();
        }
    }
}
