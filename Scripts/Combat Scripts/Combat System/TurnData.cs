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
        public CharacterDirection direction = CharacterDirection.NONE; //direction facing
        public SkillContainer skill;
        
        public Array<BattleCharacter> targets;
        public BattleGrid grid;

        public int totalHpDamageDealt { get; private set; } = 0;
        public int totalAmpDamageDealt { get; private set; } = 0;
        public int totalAmountHealed { get; private set; } = 0;
        public int previousHpDamage { get; private set; } = 0;
        public int previousAmpDamage { get; private set; } = 0;



        public TurnData(BattleCharacter caster)
        {
            this.caster = caster;
            casterStartPosition = caster.turnStartPosition;
        }

        public TurnData(BattleCharacter caster,
                                   Array<BattleCharacter> targets,
                                   SkillContainer skill)
        {
            this.caster = caster;
            this.targets = new Array<BattleCharacter>();
            this.targets.AddRange(targets);
            this.skill = skill;
        }

        public void AddHpDamage(int damage)
        {
            totalHpDamageDealt += damage;
            previousHpDamage = damage;
        }

        public void AddAmpDamage(int damage)
        {
            totalAmpDamageDealt += damage;
            previousAmpDamage = damage;
        }

        public void AddHealing(int heal)
        {
            totalAmountHealed += heal;
        }

        public void SetTargets(Array<GridSpace> spaces)
        {
            if (spaces == null) return;

            targets = new Array<BattleCharacter>();

            foreach(var space in spaces)
            {
                var target = space.CharacterOnSpace;
                if(target != null && skill.Skill.IsValidTarget(caster, target))
                {
                    targets.Add(target);
                }
            }
        }

        
    }
}
