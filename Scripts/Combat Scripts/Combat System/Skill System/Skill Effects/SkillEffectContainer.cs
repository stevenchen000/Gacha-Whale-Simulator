using Godot.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CombatSystem
{
    public class SkillEffectContainer
    {
        private SkillEffect Effect;
        public TurnData Data { get; private set; }
        public SkillCastData CastData { get; private set; }
        public BattleCharacter Target { get; private set; }

        public SkillEffectContainer(SkillEffect effect, BattleCharacter target, TurnData data, SkillCastData skillCast)
        {
            Effect = effect;
            Target = target;
            Data = data;
            CastData = skillCast;
        }


        public bool RunEffect(TimeHandler time)
        {
            return Effect.RunEffect(Data, CastData, time);
        }
    }
}
