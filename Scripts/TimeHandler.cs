using PartyMenuSystem;
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
        return currFrame >= time;
    }

    public bool TimePassed(double time)
    {
        return prevFrame >= time;
    }

    public bool TimeReached(double time)
    {
        return time < currFrame && time >= prevFrame;
    }

    public int IntervalsBetween(double interval)
    {
        if (interval <= 0) return -1;
        int prevInterval = (int)(prevFrame / interval);
        int currInterval = (int)(currFrame / interval);
        return currInterval - prevInterval;
    }
    /// <summary>
    /// Returns the number of the interval reached. 
    /// Returns 1 if time == interval
    /// Returns 2 if time == 2 * interval
    /// Returns -1 if between intervals
    /// </summary>
    /// <param name="interval"></param>
    /// <returns></returns>
    public int IntervalReached(double interval)
    {
        if(interval == 0) return -1;
        int prevInterval = (int)(prevFrame / interval);
        int currInterval = (int)(currFrame / interval);

        if (prevInterval != currInterval) 
            return currInterval;
        else 
            return -1;
    }


}

