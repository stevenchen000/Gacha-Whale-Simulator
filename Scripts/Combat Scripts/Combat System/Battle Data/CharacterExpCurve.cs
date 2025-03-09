using Godot;
using Godot.Collections;
using System;


public partial class CharacterExpCurve : Node
{
    private static CharacterExpCurve instance;

    [Export] private Array<int> expCurve;

    public override void _Ready()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            QueueFree();
        }
    }

    public static int GetExpForNextLevel(int currLevel)
    {
        int result = 0;

        result = (int)(10 * currLevel * Math.Log10(Math.Pow(currLevel, currLevel)*10));

        return result;
    }
}
