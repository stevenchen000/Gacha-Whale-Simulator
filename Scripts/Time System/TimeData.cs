using Godot;
using System;

public class TimeData
{
    private int newMinute;
    private int newHour;
    private int newDay;
    private long newTime;

    private int prevMinute;
    private int prevHour;
    private int prevDay;
    private long prevTime;

    public void SetPrevTime(int prevDay, int prevHour, int prevMinute)
    {
        this.prevDay = prevDay;
        this.prevHour = prevHour;
        this.prevMinute = prevMinute;
        prevTime = TimeToNum(prevDay, prevHour, prevMinute);
    }

    public void SetNewTime(int day, int hour, int minute)
    {
        newDay = day;
        newHour = hour;
        newMinute = minute;
        newTime = TimeToNum(newDay, newHour, newMinute);
    }

    /// <summary>
    /// Checks if time is within the range
    /// exclusive start, inclusive end
    /// ignores day
    /// </summary>
    /// <param name="hour"></param>
    /// <param name="minute"></param>
    /// <returns></returns>
    public bool IsTimeInRange(int hour, int minute)
    {
        long expectedTimePrevDay = TimeToNum(prevDay, hour, minute);
        long expectedTimeNewDay = TimeToNum(newDay, hour, minute);

        return IsTimeInRange(prevDay, hour, minute) ||
            IsTimeInRange(newDay, hour, minute);
    }

    /// <summary>
    /// Checks if time is in range
    /// exclusive start, inclusive end
    /// day is relevant
    /// </summary>
    /// <param name="day"></param>
    /// <param name="hour"></param>
    /// <param name="minute"></param>
    /// <returns></returns>
    public bool IsTimeInRange(int day, int hour, int minute)
    {
        long expectedTime = TimeToNum(day, hour, minute);
        
        return TimeBetweenRange(expectedTime, prevTime, newTime);
    }

    private long TimeToNum(int day, int hour, int minute)
    {
        return 60 * 24 * day + 60 * hour + minute;
    }

    private bool TimeBetweenRange(long time, long prevTime, long newTime)
    {
        return time > prevTime && time <= newTime;
    }
}
