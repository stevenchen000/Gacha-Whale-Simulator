using Godot;
using System;

public partial class CalendarUI : Control
{
    [Export] private Label yearText;
    [Export] private Label monthText;
    [Export] private Label dayText;
    [Export] private TextureRect timeTexture;

    [Export] private Texture2D dayTexture;
    [Export] private Texture2D noonTexture;
    [Export] private Texture2D nightTexture;

    public override void _Ready()
    {
        CallDeferred(MethodName.UpdateUI);
    }

    public override void _Process(double delta)
    {
        
    }

    public void UpdateUI()
    {
        SetTimeTexture();
        SetYearText();
        SetMonthText();
        SetDayText();
    }



    public void SetTimeTexture()
    {
        var time = GameState.GetTimeOfDay();
        Texture2D texture = null;

        switch (time)
        {
            case TimeSystem.TimeOfDay.MORNING:
                texture = dayTexture;
                break;
            case TimeSystem.TimeOfDay.AFTERNOON:
                texture = noonTexture;
                break;
            case TimeSystem.TimeOfDay.NIGHT:
                texture = nightTexture;
                break;
        }

        timeTexture.Texture = texture;
    }

    public void SetYearText()
    {
        int year = GameState.GetYear();
        yearText.Text = $"Year {year}";
    }

    public void SetMonthText()
    {
        int month = GameState.GetMonth();
        monthText.Text = $"Month {month}";
    }

    private void SetDayText()
    {
        int day = GameState.GetDay();
        dayText.Text = $"Day {day}";
    }
}
