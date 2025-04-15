using System;
using System.Collections.Generic;
using Godot;

namespace CombatSystem
{
    public partial class CombatExpDisplay : Control
    {
        private List<LevelUpData> levelUps;
        [Export] private PackedScene characterDisplayScene;
        [Export] private Node grid;
        private List<CharacterExpUi> displays = new List<CharacterExpUi> ();

        public void Init(List<LevelUpData> levelUps)
        {
            this.levelUps = levelUps;
            RemoveChildren();
            AddDisplays();
        }

        private void AddDisplays()
        {
            for (int i = 0; i < levelUps.Count; i++)
            {
                var data = levelUps[i];
                var newDisplay = Utils.InstantiateCopy<CharacterExpUi>(characterDisplayScene);
                displays.Add(newDisplay);
                grid.AddChild(newDisplay);
                newDisplay.Init(data);
            }
        }

        private void RemoveChildren()
        {
            var children = grid.GetChildren();
            foreach (var child in children)
            {
                child.QueueFree();
            }
        }
    }
}
