using Godot;
using System;
using Godot.Collections;

namespace GachaSystem{
    [GlobalClass]
    public partial class GachaBanner : Resource
    {
        [Export] private GachaCharacter rateUpCharacter;
        [Export] private float chanceForRateUpCharacter = 0.05f;

        [Export] private float chanceForSSR = 0.1f;
        [Export] private float chanceForSR = 0.3f;
        private Random rng;


        public GachaCharacter PullRandomCharacter(GachaCharacterList characterList){
            InitRNGIfNull();

            GachaCharacter result = null;

            float rand = (float)rng.NextDouble();
            if(rand < chanceForSSR){
                if(rand < chanceForRateUpCharacter){
                    result = rateUpCharacter;
                }else{
                    result = PullCharacterFromList(characterList.allSSRCharacters);
                }
            }else if(rand < chanceForSSR + chanceForSR){
                result = PullCharacterFromList(characterList.allSRCharacters);
            }else{
                result = PullCharacterFromList(characterList.allRCharacters);
            }

            GameState.GetEventManager().RaiseCharacterPulledEvent(result);

            return result;
        }

        private void InitRNGIfNull(){
            if(rng == null){
                rng = new Random(Guid.NewGuid().GetHashCode());
            }
        }

        private GachaCharacter PullCharacterFromList(Array<GachaCharacter> characterList){
            float rand = (float)rng.NextDouble();
            int listSize = characterList.Count;
            float chance = 1f/listSize;
            int randIndex = (int)(rand/chance);

            return characterList[randIndex];
        }
    }
}