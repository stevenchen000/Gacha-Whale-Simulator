using System;
using CombatSystem;
using Godot;
using Godot.Collections;


public partial class LimitBreakAmounts : Node
{
    private static LimitBreakAmounts instance;

    [Export] private Array<LimitBreakRarity> rarityList;

    [Export] private string description = "index = level before LB";

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


    public static int GetCopiesNeeded(CharacterData character)
    {
        int result = -1;

        foreach(var list in instance.rarityList)
        {
            if(list.BaseRarity == character.BaseRarity)
            {
                result = list.GetCopiesNeeded(character);
            }
        }
        Utils.Print(instance, result);
        return result;
    }
}

