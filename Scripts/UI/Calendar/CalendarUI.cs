using Godot;
using System;

public partial class CalendarUI : CanvasLayer
{
    [Export] private Label dayText;
    [Export] private Label timeText;

    public override void _Ready()
    {
        SetTimeText();
        SetDayText();
    }

    public override void _Process(double delta)
    {
        SetTimeText();
        SetDayText();
    }





    private void SetTimeText()
    {
        int hour = GameState.GetHour();
        int minute = GameState.GetMinute();
        string text = $"{hour.ToString("00")}:{minute.ToString("00")}";
        timeText.Text = text;
    }

    private void SetDayText()
    {
        int day = GameState.GetDay();
        dayText.Text = $"Day {day}";
    }
}
