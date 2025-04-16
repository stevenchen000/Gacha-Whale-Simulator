using System;
using System.Collections.Generic;
using Godot.Collections;
using Godot;

namespace CombatSystem
{
    public partial class CombatExpDisplay : Control
    {
        private List<LevelUpData> levelUps;
        private Node grid;
        [Export] private Array<CharacterExpUi> displays = new Array<CharacterExpUi> ();

        public void Init(List<LevelUpData> levelUps)
        {
            this.levelUps = levelUps;

            for (int i = 0; i < displays.Count; i++)
            {
                var display = displays[i];

                if (i < levelUps.Count)
                {
                    var levelUp = levelUps[i];
                    display.Init(levelUp);
                }
                else
                {
                    display.Visible = false;
                }
            }
        }

        public void OnClick()
        {
            if (displays[0].HasFinished)
                QueueFree();
        }
    }
}
