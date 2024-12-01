using Godot;
using System;

[GlobalClass]
public partial class TriggerLoadScene : TriggerEffect
{
    [Export] private PackedScene scene;

    public override void ActivateEffect()
    {
        SceneManager.LoadScene(scene.ResourcePath);
    }
}
