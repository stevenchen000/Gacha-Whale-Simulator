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
        [Export] public TargetType targetType { get; private set; }

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

        public bool RunEffects(TurnData data, TimeHandler time, double delta)
        {
            return true;
        }

        public bool IsValidTarget(BattleCharacter caster, BattleCharacter target)
        {
            bool result = false;

            switch (targetType)
            {
                case TargetType.Enemy:
                    result = caster.IsEnemy(target);
                    break;
                case TargetType.Ally:
                    result = !caster.IsEnemy(target);
                    break;
            }

            return result;
        }


        public Dictionary<CharacterDirection, Array<GridSpace>> GetAllTargetSpaces(BattleManager battle,
                                                                                   BattleGrid grid, 
                                                                                   BattleCharacter caster)
        {
            var result = new Dictionary<CharacterDirection, Array<GridSpace>>();

            var up = CharacterDirection.UP;
            var down = CharacterDirection.DOWN;
            var left = CharacterDirection.LEFT;
            var right = CharacterDirection.RIGHT;

            switch (direction)
            {
                case SkillDirection.VERTICAL:
                    AddTargetsInDirectionIfExists(result, battle, grid, caster, up);
                    AddTargetsInDirectionIfExists(result, battle, grid, caster, down);
                    break;
                case SkillDirection.HORIZONTAL:
                    AddTargetsInDirectionIfExists(result, battle, grid, caster, left);
                    AddTargetsInDirectionIfExists(result, battle, grid, caster, right);
                    break;
                case SkillDirection.ALL:
                    AddTargetsInDirectionIfExists(result, battle, grid, caster, up);
                    AddTargetsInDirectionIfExists(result, battle, grid, caster, down);
                    AddTargetsInDirectionIfExists(result, battle, grid, caster, left);
                    AddTargetsInDirectionIfExists(result, battle, grid, caster, right);
                    break;
            }

            return result;
        }

        private void AddTargetsInDirectionIfExists(Dictionary<CharacterDirection, Array<GridSpace>> spaces,
                                                   BattleManager battle, 
                                                   BattleGrid grid, 
                                                   BattleCharacter caster, 
                                                   CharacterDirection direction)
        {
            var results = GetSpacesInDirection(grid, caster, direction);
            if (HasTargetsInList(battle, caster, results))
                spaces[direction] = results;
        }

        private bool HasTargetsInList(BattleManager battle, BattleCharacter caster, Array<GridSpace> spaces)
        {
            bool result = false;

            foreach(var space in spaces)
            {
                var target = space.CharacterOnSpace;
                if (target != null)
                {
                    result = IsValidTarget(caster, target);
                    if (result)
                        break;
                }
            }

            return result;
        }

        private Array<GridSpace> GetSpacesInDirection(BattleGrid grid, 
                                                    BattleCharacter caster, 
                                                    CharacterDirection direction)
        {
            Array<GridSpace> result = new Array<GridSpace>();
            var attackPositions = AttackArea.GetPositionsInRange(caster.currPosition, direction);
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