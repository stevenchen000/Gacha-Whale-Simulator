using Godot;
using System;

public partial class DateTimeManager : Node
{
    [Export] public int day { get; private set; } = 1;
    [Export] public int hour { get; private set; } = 8;
    [Export] public int minute { get; private set; } = 0;
    [Export] private int secondsPerUpdate = 10;
    [Export] private int minutesPerUpdate = 5;

    private double seconds = 0;
    private int timeLock = 0;

    public override void _Process(double delta)
    {
        if (!IsTimePaused())
        {
            Tick(delta);
        }
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
            GD.PrintErr("WARNING: Time lock is negative");
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
            minute += minutesPerUpdate;
            seconds -= secondsPerUpdate;
            hour += minute / 60;
            minute = minute % 60;

            day += hour / 24;
            hour %= 24;
        }
    }
}
