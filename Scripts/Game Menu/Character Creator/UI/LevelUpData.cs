using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CombatSystem
{
    public class LevelUpData
    {
        public CharacterData Character { get; private set; }
        public int PreviousLevel { get; private set; }
        public int NewLevel { get; private set; }


        public double PreviousExpPercent { get; private set; }
        public double NewExpPercent { get; private set; }

        public LevelUpData(CharacterData character, 
                           int previousLevel, int newLevel, 
                           double previousExpPercent, double newExpPercent) 
        {
            Character = character;
            PreviousLevel = previousLevel;
            NewLevel = newLevel;
            PreviousExpPercent = previousExpPercent;
            NewExpPercent = newExpPercent;
        }
    }
}
