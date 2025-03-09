using Godot;
using System;
using Godot.Collections;

namespace GachaSystem
{
    [GlobalClass]
    public partial class GachaCharacterList : Resource
    {
        [Export] public string Name { get; private set; }
        [Export] public int Weight { get; private set; }
        [Export] public Array<GameCharacter> Characters {get; private set;}

        public GameCharacter GetRandomCharacter()
        {
            int maxIndex = Characters.Count - 1;
            int randIndex = GameState.GetRandomNumber(0, maxIndex);

            return Characters[randIndex];
        }

        public bool IsEmpty()
        {
            return Characters.Count == 0;
        }
    }
}