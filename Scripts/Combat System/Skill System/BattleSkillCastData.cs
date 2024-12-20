using System;
using Godot;
using Godot.Collections;

namespace CombatSystem
{
    public class BattleSkillCastData
    {
        public BattleCharacter caster;
        public Vector2I casterPosition;
        public CharacterSkill skill;
        
        public Array<BattleCharacter> targets;
        public BattleGrid grid;
        public double prevFrame { get; private set;  } = 0;
        public double currFrame { get; private set; } = 0;


        public BattleSkillCastData(BattleCharacter caster,
                                   Array<BattleCharacter> targets,
                                   CharacterSkill skill)
        {

        }

        public void Tick(double delta)
        {
            prevFrame = currFrame;
            currFrame += delta;
        }

        
    }
}
