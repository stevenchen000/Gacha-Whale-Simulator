using Godot;
using System;

[GlobalClass]
public partial class TriggerLoadScene : TriggerEffect
{
    [Export] private PackedScene scene;

    public override void ActivateEffect(TriggerDetector activator, Trigger trigger)
    {
        SceneManager.LoadScene(scene.ResourcePath);
    }
}
