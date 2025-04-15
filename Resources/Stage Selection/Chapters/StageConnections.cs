using Godot;
using Godot.Collections;
using System;

public partial class StageConnections : Node
{
    [Export] private Line2D lineBorder;
    [Export] private Line2D lineCenter;

    private Array<Vector2> points = new Array<Vector2> ();
    private int unlockedIndex = -1;

    public void Init(Array<ChapterSelectionButton> nodes)
    {
        for(int i = 0; i < nodes.Count; i++)
        {
            var node = nodes[i];
            points.Add(node.Position);
            if(node.NeedsToPlayUnlockAnimation() && unlockedIndex == -1)
            {
                unlockedIndex = i;
            }
        }
    }
}
