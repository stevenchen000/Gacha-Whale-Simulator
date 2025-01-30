using Godot;
using System;
using Godot.Collections;

namespace CombatSystem {
    [GlobalClass]
    public partial class CharacterSkill : Resource
    {
        [Export] public Texture2D Icon { get; private set; }
        [Export] public SkillAnimation animation { get; private set; }
        [Export] public Array<SkillEffect> effects { get; private set; }
        [Export] public float duration { get; private set; } = 2f;
        [Export] public int potency { get; private set; } = 100;
        [Export] public GridShape AttackArea { get; private set; }
        [Export] public SkillDirection direction { get; private set; }

        public CharacterSkill GetDuplicate()
        {
            var newSkill = new CharacterSkill();
            newSkill.animation = animation.GetDuplicate();
            //duplicate skill effects
            newSkill.duration = duration;
            newSkill.potency = potency;
            newSkill.AttackArea = AttackArea;

            return newSkill;
        }

        public bool PlayAnimation(TurnData data, TimeHandler time, double delta)
        {
            return animation.PlayAnimation(data, time, delta);
        }

        public bool RunEffects(TurnData data, double delta)
        {
            return true;
        }


        public Dictionary<CharacterDirection, Array<GridSpace>> GetAllTargetSpaces(BattleGrid grid, 
                                                                                   BattleCharacter caster)
        {
            var result = new Dictionary<CharacterDirection, Array<GridSpace>>();

            switch (direction)
            {
                case SkillDirection.VERTICAL:
                    var up = CharacterDirection.UP;
                    var down = CharacterDirection.DOWN;
                    result[up] = GetSpacesInDirection(grid, caster, up);
                    result[down] = GetSpacesInDirection(grid, caster, down);
                    break;
                case SkillDirection.HORIZONTAL:
                    var left = CharacterDirection.LEFT;
                    var right = CharacterDirection.RIGHT;
                    result[left] = GetSpacesInDirection(grid, caster, left);
                    result[right] = GetSpacesInDirection(grid, caster, right);
                    break;
            }

            return result;
        }


        private Array<GridSpace> GetSpacesInDirection(BattleGrid grid, 
                                                    BattleCharacter caster, 
                                                    CharacterDirection direction)
        {
            Array<GridSpace> result = new Array<GridSpace>();
            var attackPositions = AttackArea.GetPositionsInRange(caster.tempPosition, direction);
            foreach (var position in attackPositions)
            {
                var space = grid.GetSpaceFromCoords(position);
                if(space != null)
                {
                    result.Add(space);
                }
            }

            return result;
        }
    }
}