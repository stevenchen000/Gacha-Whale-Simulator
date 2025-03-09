using Godot;
using Godot.Collections;
using System;

public partial class CharacterRarityDisplay : Node
{
    [Export] private Array<RarityStar> stars;

    public void SetStars(int count)
    {
        for(int i = 0; i < stars.Count; i++)
        {
            if(i < count)
                stars[i].SetActive(true);
            else
                stars[i].SetActive(false);
        }
    }
}
