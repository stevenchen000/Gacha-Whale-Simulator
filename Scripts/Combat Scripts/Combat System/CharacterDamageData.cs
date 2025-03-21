using System;

namespace CombatSystem
{
    public class CharacterDamageData
    {
        public int TotalHpDamage { get; private set; }
        public int TotalAmpDamage { get; private set; }
        public int PreviousHpDamage { get; private set; }
        public int PreviousAmpDamage { get; private set; }


        public int PreviousTurn { get; private set; }
        public int PreviousTurnHpDamage { get; private set; }
        public int PreviousTurnAmpDamage { get; private set; }


        public void AddHpDamage(int damage, int turn)
        {
            if(PreviousTurn != turn)
            {
                PreviousTurn = turn;
                PreviousTurnHpDamage = damage;
                PreviousTurnAmpDamage = 0;
            }
            else
            {
                PreviousTurnHpDamage += damage;
            }
            TotalHpDamage += damage;
            PreviousHpDamage = damage;
        }

        public void AddAmpDamage(int damage, int turn)
        {
            if (PreviousTurn != turn)
            {
                PreviousTurn = turn;
                PreviousTurnAmpDamage = damage;
                PreviousTurnHpDamage = 0;
            }
            else
            {
                PreviousTurnAmpDamage += damage;
            }
            TotalAmpDamage += damage;
            PreviousAmpDamage = damage;
        }
    }
}
