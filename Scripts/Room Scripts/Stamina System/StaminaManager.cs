using System;
using EventSystem;
using Godot;

namespace StaminaSystem
{
    public partial class StaminaManager : Node, ISavable
    {
        [Export] public int MaxStamina { get; private set; } = 50;
        [Export] public int CurrentStamina { get; private set; } = 50;

        [ExportCategory("Events")]
        [Export] private VoidEvent OnStaminaChanged;

        public void UseStamina(int amount)
        {
            CurrentStamina -= amount;
            OnStaminaChanged.RaiseEvent(this);
        }

        public void AddMaxStamina(int amount)
        {
            MaxStamina += amount;
            CurrentStamina += amount;
            OnStaminaChanged.RaiseEvent(this);
        }

        public void AddStamina(int amount)
        {
            CurrentStamina += amount;
            OnStaminaChanged.RaiseEvent(this);
        }

        public void FillStamina()
        {
            CurrentStamina = Math.Max(CurrentStamina, MaxStamina);
            OnStaminaChanged.RaiseEvent(this);
        }

        public bool HasEnoughStamina(int amount)
        {
            return CurrentStamina >= amount;
        }
        /*****************
         * Save and Load
         * **************/

        private string currentStaminaFlag = "current_stamina";
        private string maxStaminaFlag = "max_stamina";

        public void Save()
        {
            GameState.SetFlag(currentStaminaFlag, CurrentStamina);
            GameState.SetFlag(maxStaminaFlag, MaxStamina);
        }

        public void Load() 
        {
            int maxStam = GameState.GetFlagAmount(maxStaminaFlag);

            if (maxStam > 0)
            {
                CurrentStamina = GameState.GetFlagAmount(currentStaminaFlag);
                MaxStamina = maxStam;
            }
        }

    }
}
