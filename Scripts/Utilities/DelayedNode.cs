using Godot;
using System;

/// <summary>
/// Simple node that calls
/// _Init in a deferred call
/// </summary>
public partial class DelayedNode : Node
{

    public override void _Ready()
    {
        CallDeferred(MethodName._Init);
    }

    protected virtual void _Init()
    {

    }
}
