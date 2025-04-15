using Godot;
using System;
using System.Collections.Generic;

public partial class DelayedCalls : Node
{
    private static DelayedCalls _instance;

    private List<CallWrapper> _calls = new List<CallWrapper>();


    public override void _Ready()
    {
        if(_instance == null)
        {
            _instance = this;
        }
        else
        {
            QueueFree();
        }
    }

    public static void AddCall(double delay,  CallWrapper.CallDelegate call)
    {
        _instance._calls.Add(new CallWrapper(delay, call));
    }

    public override void _Process(double delta)
    {
        int index = 0;

        while (index < _calls.Count)
        {
            var call = _calls[index];
            bool finished = call.Run(delta);
            if (finished)
            {
                _calls.RemoveAt(index);
            }
            else
            {
                index++;
            }
        }
    }
}
