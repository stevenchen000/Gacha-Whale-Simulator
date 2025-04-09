using Godot;
using System;

public partial class StackableMenu : Control
{
    [Export] private AnimationPlayer anim;
    [Export] private double closeTime = 0.3;
    private TimeHandler time = new TimeHandler();
    private bool closing = false;

    private MenuStack stack;



    public override void _Process(double delta)
    {
        if (closing)
        {
            time.Tick(delta);
            if (time.TimeReached(closeTime))
            {
                QueueFree();
            }
        }
    }

    public virtual void InitMenu(MenuStack stack)
    {
        this.stack = stack;
    }

    public virtual void CloseMenu() 
    {
        anim.Play("pop_out");
        closing = true;
    }

    public void OnBackgroundClick()
    {
        if (!closing)
            stack.CloseCurrentMenu();
    }


}
