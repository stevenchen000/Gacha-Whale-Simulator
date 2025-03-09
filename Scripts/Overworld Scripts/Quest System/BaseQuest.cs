using Godot;
using System;
using Godot.Collections;

namespace QuestSystem
{
    [GlobalClass]
    public partial class BaseQuest : Resource
    {
        [Export] public string name;
        [Export] public string description;
        [Export] public Array<QuestGoal> goals { get; set; }
    }
}