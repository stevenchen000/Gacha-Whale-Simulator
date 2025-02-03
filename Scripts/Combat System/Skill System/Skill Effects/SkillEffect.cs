using Godot;
using System;

namespace CombatSystem {
    [GlobalClass]
    public partial class SkillEffect : Resource
    {
        [Export] protected float delay = 0f;

        public bool RunEffect(TurnData data, TimeHandler timer) {
            bool finished = false;
            
            if (timer.TimeIsBetween(delay))
                _StartEffect(data);
            if(timer.TimeIsUp(delay))
                finished = _RunEffect(data, timer);

            if(finished) _EndEffect(data);

            return finished;
        }

        protected virtual void _StartEffect(TurnData data) { }

        protected virtual bool _RunEffect(TurnData data, TimeHandler timer) { return true; }
        protected virtual void _EndEffect(TurnData data) { }
    }
}