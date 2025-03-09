using Godot;
using System;

public partial class RarityStar : Node
{
    [Export] private Control activeStar;
    [Export] private Control inactiveStar;
    [Export] private bool isActive = false;

    public void SetActive(bool active)
    {
        isActive = active;
        if (isActive)
        {
            inactiveStar.Visible = false;
            activeStar.Visible = true;
        }
        else
        {
            inactiveStar.Visible = true;
            activeStar.Visible = false;
        }
    }
}
