using System;
using Godot;
using Godot.Collections;

[GlobalClass]
public partial class LimitBreakDict : Resource
{
    [Export] private Dictionary<int, int> dict = new Dictionary<int, int>();

    public int GetCopiesNeeded(int currentLB)
    {
        int result = -1;
        Utils.Print(this, dict.Keys);
        try
        {
            result = dict[currentLB];
        }
        catch (Exception ex)
        {
        }
        return result;
    }
}

