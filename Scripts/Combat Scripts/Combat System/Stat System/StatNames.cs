using CombatSystem;
using Godot;
using System;

public partial class StatNames : Node
{
    private static StatNames instance;

    [Export] private StatType _health;
    public static StatType Health
    {
        get { return instance._health; }
    }

    [Export] private StatType _amp;
    public static StatType Amp
    {
        get { return instance._amp; }
    }

    [Export] private StatType _attack;
    public static StatType Attack
    {
        get { return instance._attack; }
    }

    [Export] private StatType _defense;
    public static StatType Defense
    {
        get { return instance._defense; }
    }

    [Export] private StatType _spirit;
    public static StatType Spirit
    {
        get { return instance._spirit; }
    }

    [Export] private StatType _speed;
    public static StatType Speed
    {
        get { return instance._speed; }
    }

    [Export] private StatType _movement;
    public static StatType Movement
    {
        get { return instance._movement; }
    }


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
}
