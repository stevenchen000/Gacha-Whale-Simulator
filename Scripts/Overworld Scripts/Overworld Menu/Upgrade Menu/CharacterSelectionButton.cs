using System;
using CombatSystem;
using Godot;


public partial class CharacterSelectionButton : Button
{
    [Export] private CharacterPortraitDisplay display;
    [Export] private PortraitBorder borderR;
    [Export] private PortraitBorder borderSR;
    [Export] private PortraitBorder borderSSR;

    private CharacterData data;

    public delegate void OnButtonClick(CharacterData data);
    private OnButtonClick onButtonClick;


    public override void _Ready()
    {
        base._Ready();
        Pressed += () => onButtonClick.Invoke(data);
    }

    public override void _Notification(int what)
    {
        base._Notification(what);
        if (what == NotificationPredelete)
        {
            onButtonClick = null;
        }
    }

    public void SetupData(CharacterData newData)
    {
        data = newData;
        var portrait = data.GetPortrait();
        display.UpdatePortrait(portrait);
    }

    /**************
     * Event Functions
     * ************/

    public void SubscribeEvent(OnButtonClick func)
    {
        onButtonClick += func;
    }


}

