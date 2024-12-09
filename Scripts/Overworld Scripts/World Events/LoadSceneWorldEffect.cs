using Godot;
using System;

[GlobalClass]
public partial class LoadSceneWorldEffect : WorldEventEffect
{
    [Export] private PackedScene scene;

    public override void ActivateEffect()
    {
        string scenePath = scene.ResourcePath;
        base.ActivateEffect();
        SceneManager.LoadScene(scenePath);
    }
}
