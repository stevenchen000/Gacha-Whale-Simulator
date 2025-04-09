using Godot;
using System;
using Godot.Collections;

public partial class GameFlags : Node
{
    private Dictionary<string, int> flags = new Dictionary<string, int>();

    public override void _Ready()
    {
        
    }







    public bool CheckFlag(string flag)
    {
        return GetFlagAmount(flag) > 0;
    }

    public int GetFlagAmount(string flag)
    {
        int result = 0;

        if (flags.ContainsKey(flag))
        {
            result = flags[flag];
        }

        return result;
    }

    public void ResetFlag(string flag)
    {
        flags[flag] = 0;
    }

    public void SetFlag(string flag, int amount)
    {
        flags[flag] = amount;
    }

    public void ToggleFlag(string flag)
    {
        if (CheckFlag(flag))
        {
            SetFlag(flag, 0);
        }
        else
        {
            SetFlag(flag, 1);
        }
    }

    public void AddFlag(string flag, int amount = 1)
    {
        if (!flags.ContainsKey(flag))
            flags[flag] = amount;
        else
            flags[flag] += amount;
    }

    public void RemoveFlag(string flag, int amount = 1)
    {
        if (!flags.ContainsKey(flag))
            flags[flag] = -amount;
        else
            flags[flag] -= amount;
    }


    /************************
     * Save and Load
     * ********************/

    private string filename = "user://gamedata.json";

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
            flags = (Dictionary<string, int>)Json.ParseString(json);

            file.Close();
        }
    }
}
