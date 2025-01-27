using System;
using Godot;
using Godot.Collections;

namespace CombatSystem
{
    public class TurnData
    {
        public BattleCharacter caster;
        public Vector2I casterStartPosition;
        public Vector2I casterPosition; //position where caster was standing at cast time
        public Vector2I targetLocation; //position where skill is cast
        public SkillDirection direction; //direction facing
        public CharacterSkill skill;
        
        public Array<BattleCharacter> targets;
        public BattleGrid grid;
        public double prevFrame { get; private set;  } = 0;
        public double currFrame { get; private set; } = 0;

        public int totalDamageDealt { get; private set; } = 0;
        public int totalAmountHealed { get; private set; } = 0;


        public TurnData(BattleCharacter caster)
        {
            this.caster = caster;
            casterStartPosition = caster.previousPosition;
        }

        public TurnData(BattleCharacter caster,
                                   Array<BattleCharacter> targets,
                                   CharacterSkill skill)
        {
            this.caster = caster;
            this.targets = new Array<BattleCharacter>();
            this.targets.AddRange(targets);
            this.skill = skill.GetDuplicate();
        }

        public void AddDamage(int damage)
        {
            totalDamageDealt += damage;
        }

        public void AddHealing(int heal)
        {
            totalAmountHealed += heal;
        }

        public void Tick(double delta)
        {
            prevFrame = currFrame;
            currFrame += delta;
        }

        public void ResetTime()
        {
            prevFrame = 0;
            currFrame = 0;
        }

        
    }
}
