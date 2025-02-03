using Godot;
using System;
using EventSystem;

public partial class LoadScreen : Node
{
    [Export] private TextureRect loadScreen;
    [Export] private Color loadScreenColor;
    [Export] private Color loadScreenDisabledColor;
    [Export] private float transitionTime;

    [Export] private VoidEvent OnFadeOutStarted;
    [Export] private VoidEvent OnFadeOutFinished;
    [Export] private VoidEvent OnFadeInStarted;
    [Export] private VoidEvent OnFadeInFinished;

    [Export] private VoidEvent OnSceneLoad;

    private LoadScreenState state = LoadScreenState.FADING_IN;
    private float transitionTimer = 0;

    public override void _Ready()
    {
        Init();
    }

    public override void _Process(double delta)
    {
        RunState(delta);
    }

    public void Init()
    {
        loadScreen.Position = new Vector2(0, 0);
        SetScreenColor(loadScreenColor);
        ChangeState(LoadScreenState.FADING_IN);
        //Called by Scene Manager when the actual scene swapping happens
        OnSceneLoad?.SubscribeEvent(() => ChangeState(LoadScreenState.FADING_IN));
    }

    public void Activate()
    {
        ChangeState(LoadScreenState.FADING_OUT);
    }

    private void RunState(double delta)
    {
        switch (state)
        {
            case LoadScreenState.FADING_OUT:
                TransitionToColor(loadScreenColor);
                ChangeStateAtTime(LoadScreenState.LOADING_SCENE, transitionTime, delta);
                break;
            case LoadScreenState.FADING_IN:
                TransitionToColor(loadScreenDisabledColor);
                ChangeStateAtTime(LoadScreenState.INACTIVE, transitionTime, delta);
                break;
            case LoadScreenState.LOADING_SCENE:
                break;
            case LoadScreenState.INACTIVE:
                break;
        }

    }

    private void ChangeState(LoadScreenState newState)
    {
        switch (state)
        {
            case LoadScreenState.FADING_OUT:
                break;
            case LoadScreenState.FADING_IN:
                break;
            case LoadScreenState.LOADING_SCENE:
                break;
            case LoadScreenState.INACTIVE:
                break;
        }
        switch (newState)
        {
            case LoadScreenState.FADING_OUT:
                OnFadeOutStarted?.RaiseEvent(this);
                break;
            case LoadScreenState.FADING_IN:
                OnFadeInStarted?.RaiseEvent(this);
                break;
            case LoadScreenState.LOADING_SCENE:
                OnFadeOutFinished?.RaiseEvent(this);
                SetScreenColor(loadScreenColor);
                transitionTimer = 0;
                break;
            case LoadScreenState.INACTIVE:
                OnFadeInFinished?.RaiseEvent(this);
                SetScreenColor(loadScreenDisabledColor);
                transitionTimer = 0;
                break;
        }

        state = newState;
        Utils.Print(this, state);
    }


    private void TransitionToColor(Color color)
    {
        var screenColor = loadScreen.Modulate;
        screenColor = screenColor.Lerp(color, 0.02f);
        loadScreen.Modulate = screenColor;
    }

    private void SetScreenColor(Color color)
    {
        loadScreen.Modulate = color;
    }

    private void ChangeStateAtTime(LoadScreenState newState, float time, double delta)
    {
        if(transitionTimer > time)
        {
            ChangeState(newState);
            transitionTimer = 0;
        }
        else
        {
            transitionTimer += (float)delta;
        }
    }
}
