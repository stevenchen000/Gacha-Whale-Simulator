using System;


public class TimeHandler
{
    public double prevFrame { get; private set; } = 0;
    public double currFrame { get; private set; } = 0;

    public TimeHandler() { }

    public void Tick(double delta)
    {
        prevFrame = currFrame;
        currFrame += delta;
    }

    public bool TimeIsBetween(double time)
    {
        return time >= prevFrame && time < currFrame;
    }

    public bool TimeIsUp(double time)
    {
        return time >= currFrame;
    }

    public int IntervalsBetween(double interval)
    {
        if (interval <= 0) return -1;
        int prevInterval = (int)(prevFrame / interval);
        int currInterval = (int)(currFrame / interval);
        return currInterval - prevInterval;
    }


}

