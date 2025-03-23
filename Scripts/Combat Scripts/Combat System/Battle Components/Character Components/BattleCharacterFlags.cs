using System;
using Godot;
using Godot.Collections;

namespace CombatSystem
{
    public partial class BattleCharacterFlags : Node
    {
        private Dictionary<string, int> _flags = new Dictionary<string, int>();

        [Signal]
        public delegate void CharacterStateChangedEventHandler();

        public bool GetFlag(string flag)
        {
            AddMissingFlag(flag);
            return _flags[flag] > 0;
        }

        public int GetFlagAmount(string flag)
        {
            AddMissingFlag(flag);
            return _flags[flag];
        }

        public void AddFlag(string flag, int amount = 1)
        {
            AddMissingFlag(flag);
            _flags[flag] += amount;
            RaiseEvent();
        }

        public void RemoveFlag(string flag, int amount = 1)
        {
            AddMissingFlag(flag);
            _flags[flag] -= amount;
            RaiseEvent();
        }

        public void SetFlagValue(string flag, int value)
        {
            AddMissingFlag(flag);
            _flags[flag] = value;
            RaiseEvent();
        }



        private void AddMissingFlag(string flag)
        {
            if (!_flags.ContainsKey(flag))
            {
                _flags[flag] = 0;
            }
        }

        /****************
         * Event
         * **************/

        private void RaiseEvent()
        {
            EmitSignal(SignalName.CharacterStateChanged);
        }
    }
}
