using Godot;
using System;
using Godot.Collections;

public partial class GameFlags : Node
{
    private Dictionary<string, int> flags;

    public override void _Ready()
    {
        flags = new Dictionary<string, int>();
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
}
