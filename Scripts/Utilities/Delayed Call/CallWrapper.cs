using Godot;
using System;

public class CallWrapper
{
    public delegate void CallDelegate();

    private CallDelegate callDelegate;
    private double delay;
    private TimeHandler time;

    public CallWrapper(double delay, CallDelegate func)
    {
        this.delay = delay;
        callDelegate = func;
        time = new TimeHandler();
    }

    public bool Run(double delta)
    {
        time.Tick(delta);
        bool timeIsUp = time.TimeReached(delay);

        if (timeIsUp)
        {
            callDelegate.Invoke();
        }

        return timeIsUp;
    }
}
