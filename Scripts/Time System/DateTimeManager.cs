using Godot;
using System;

public partial class DateTimeManager : Node
{
    [Export] public int day { get; private set; } = 1;
    [Export] public int hour { get; private set; } = 8;
    [Export] public int minute { get; private set; } = 0;
    [Export] private int secondsPerUpdate = 10;
    [Export] private int minutesPerUpdate = 5;

    [Export] private TimePassedEvent OnTimeUpdate;
    [Export] private TimePassedEvent OnTimeSkipped;

    private double seconds = 0;
    private int timeLock = 0;

    public override void _Process(double delta)
    {
        /*if (!IsTimePaused())
        {
            Tick(delta);
        }*/
    }

    public void SkipTime(int hours, int minutes)
    {
        var timeData = new TimeData();
        timeData.SetPrevTime(day, hour, minute);
        IncrementTime(hours * 60 + minutes);
        seconds = 0;
        timeData.SetNewTime(day, hour, minute);
        OnTimeSkipped?.RaiseEvent(this,timeData);
    }

    public void SkipToTime(int newHour, int newMinute)
    {
        var timeData = new TimeData();
        timeData.SetPrevTime(day, hour, minute);
        SetTimeTo(newHour, newMinute);
        timeData.SetNewTime(day, hour, minute);
        OnTimeSkipped?.RaiseEvent(this,timeData);
    }

    public void SpeedupTime()
    {

    }

    public void PauseTime()
    {
        timeLock++;
    }

    public void UnpauseTime()
    {
        timeLock--;
        if(timeLock < 0) 
            Utils.Print(this, "WARNING: Time lock is negative");
    }

    private bool IsTimePaused()
    {
        return timeLock > 0;
    }

    private void Tick(double delta)
    {
        seconds += delta;
        if(seconds > secondsPerUpdate)
        {
            var timeData = new TimeData();
            seconds -= secondsPerUpdate;
            timeData.SetPrevTime(day, hour, minute);
            IncrementTime(minutesPerUpdate);
            timeData.SetNewTime(day, hour, minute);
            OnTimeUpdate?.RaiseEvent(this,timeData);
        }
    }

    private void IncrementTime(int minutes)
    {
        minute += minutes;
        hour += minute / 60;
        minute = minute % 60;

        day += hour / 24;
        hour %= 24;
    }

    private void SetTimeTo(int newHour, int newMinute)
    {
        if(!TimeIsInFuture(newHour, newMinute))
        {
            day++;
        }
        hour = newHour;
        minute = newMinute;
    }

    private bool TimeIsInFuture(int hour, int minute)
    {
        long currTime = 60 * this.hour + this.minute;
        long newTime = 60 * hour + minute;
        return newTime > currTime;
    }
}
