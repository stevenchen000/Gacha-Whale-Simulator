using Godot;
using System;

namespace CombatSystem {
    [GlobalClass]
    public partial class SkillEffect : Resource
    {
        [Export] private float delay = 0f;

        public bool RunEffect() {

            return true;
        }

        protected virtual void _StartEffect(TurnData data) { }

        protected virtual bool _RunEffect(TurnData data) { return true; }
        protected virtual void _EndEffect(TurnData data) { }
    }
}