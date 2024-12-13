using Godot;
using System;
using InventorySystem;

namespace QuestSystem
{
    
    public partial class QuestGoal : Resource
    {
        [Export] public int goalAmount { get; protected set; } = 1;
        [Export] public int progress { get; protected set; } = 0;

        public virtual void OnQuestStart() { }

        public virtual void SetupListeners() { }

        public virtual void RemoveListeners() { }

        public bool IsCompleted() { return progress >= goalAmount; }

    }
}