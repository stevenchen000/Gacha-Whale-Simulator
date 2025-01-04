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

        protected virtual void _StartEffect(BattleSkillCastData data) { }

        protected virtual bool _RunEffect(BattleSkillCastData data) { return true; }
        protected virtual void _EndEffect(BattleSkillCastData data) { }
    }
}