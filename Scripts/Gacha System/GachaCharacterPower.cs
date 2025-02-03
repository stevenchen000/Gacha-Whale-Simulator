using Godot;
using System;
using Godot.Collections;

namespace GachaSystem {
    
    public partial class GachaCharacterPower : Node
    {
        [Export] Array<GachaValuePair> characterPowers;
        [Export] bool addAllCharacters = false;
        [Export] GameState gs;
        [Export] private Dictionary<GachaCharacter, int> powerValues;
        private Random rng;

        public override void _Ready()
        {
            
        }

        public override void _Process(double delta)
        {
            if (Engine.IsEditorHint())
            {
                if (addAllCharacters)
                {
                    addAllCharacters = false;
                    var allCharacters = gs.game.allCharacters;

                    Utils.Print(this, 4);
                    if (rng == null)
                    {

                        Utils.Print(this, 5);
                        rng = new Random(Guid.NewGuid().GetHashCode());
                    }

                    if(allCharacters != null)
                        AddAllMissingCharacters(allCharacters);
                }
            }
        }


        private void AddAllMissingCharacters(GachaCharacterList list)
        {
            if(list != null)
            {
                var SSRCharacters = list.allSSRCharacters;
                AddAllCharactersFromList(SSRCharacters);
            }
        }

        private void AddAllCharactersFromList(Array<GachaCharacter> characters)
        {
            foreach(var character in characters)
            {
                int index = characterPowers.Count;
                if (!powerValues.ContainsKey(character))
                {
                    int power = CalculatePowerCreep(index);
                    powerValues[character] = power;
                    characterPowers.Add(new GachaValuePair(character, power));
                }
            }
        }

        private void InitDictionary()
        {
            powerValues = new Dictionary<GachaCharacter, int>();
            foreach(var characterPower in characterPowers)
            {
                var character = characterPower.character;
                int power = characterPower.power;
                powerValues[character] = power;
            }
        }

        private int CalculatePowerCreep(int index)
        {
            double random = rng.NextDouble() * 0.2 - 0.1;
            return (int)(1000 * (1 + random) * Math.Pow(1.05, index));
        }
    }

    
}