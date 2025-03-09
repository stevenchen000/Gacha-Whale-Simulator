using System;
using Godot;

[GlobalClass]
public partial class StageReward : Resource
{
    public virtual void ReceiveReward() { }
    public virtual Texture2D GetTexture() { return null; }
    public virtual int GetAmount() { return 0; }
}
