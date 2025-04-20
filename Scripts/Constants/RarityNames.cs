using Godot;
using System;

public partial class RarityNames : Node
{
    private static RarityNames instance;

    [Export] private Texture2D nRarity;
    [Export] private Texture2D rRarity;
    [Export] private Texture2D srRarity;
    [Export] private Texture2D ssrRarity;
    


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


    public static Texture2D GetRarityIcon(CharacterRarity rarity)
    {
        Texture2D result = null;

        switch (rarity)
        {
            case CharacterRarity.N:
                result = instance.nRarity;
                break;
            case CharacterRarity.R:
                result = instance.rRarity;
                break;
            case CharacterRarity.SR:
                result = instance.srRarity;
                break;
            case CharacterRarity.SSR:
                result = instance.ssrRarity;
                break;
        }

        return result;
    }
}
