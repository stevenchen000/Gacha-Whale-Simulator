using Godot;
using System;

[GlobalClass]
public partial class DateTime : Resource
{
    [Export] private int day {get; set;}
    [Export] private int hour {get; set;}
    [Export] private int minute {get; set;}
    [Export] private float secondsPerFiveMinutes {get; set;} = 5;
    private float tick = 0;

    public void Tick(float delta){
        tick += delta;
        while(tick > secondsPerFiveMinutes){
            tick -= secondsPerFiveMinutes;
            AddTime(0,5);
        }
    }

    public void AddTime(int hours, int minutes){
        minute += minutes;
        while(minute > 60){
            minute -= 60;
            hour++;
        }

        hour += hours;
        while(hour > 24){
            hour -= 24;
            day++;
        }

    }

}
