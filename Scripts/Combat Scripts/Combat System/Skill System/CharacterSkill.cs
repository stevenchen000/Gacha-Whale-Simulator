using Godot;
using System;
using Godot.Collections;

namespace CombatSystem {
    [GlobalClass]
    public partial class CharacterSkill : Resource
    {
        [Export] public Texture2D Icon { get; private set; }
        [Export] public string SkillName { get; private set; }
        [Export] public SkillAnimation animation { get; private set; }
        [Export] public float duration { get; private set; } = 2f;


        [ExportCategory("Damage & Effects")]
        [Export] public bool InfiniteUses { get; private set; } = false;
        [Export] public int Uses { get; private set; } = 3;
        [Export] public Element SkillElement { get; private set; }
        [Export] public Array<SkillEffect> effects { get; private set; }
        [Export] public int potency { get; private set; } = 100;
        [Export] public GridShape AttackArea { get; private set; }
        [Export] public SkillDirection direction { get; private set; }
        [Export] public TargetType targetType { get; private set; }



        [ExportCategory("Charge Skill Data")]
        [Export] public bool IsChargeSkill { get; private set; } = false;
        [Export] public int ChargeAmount { get; private set; } = 100;


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
                                                                                   BattleCharacter caster,
                                                                                   Vector2I position)
        {
            var result = new Dictionary<CharacterDirection, Array<GridSpace>>();

            var up = CharacterDirection.UP;
            var down = CharacterDirection.DOWN;
            var left = CharacterDirection.LEFT;
            var right = CharacterDirection.RIGHT;

            switch (direction)
            {
                case SkillDirection.VERTICAL:
                    AddTargetsInDirectionIfExists(result, battle, grid, caster, position, up);
                    AddTargetsInDirectionIfExists(result, battle, grid, caster, position, down);
                    break;
                case SkillDirection.HORIZONTAL:
                    AddTargetsInDirectionIfExists(result, battle, grid, caster, position, left);
                    AddTargetsInDirectionIfExists(result, battle, grid, caster, position, right);
                    break;
                case SkillDirection.ALL:
                    AddTargetsInDirectionIfExists(result, battle, grid, caster, position, up);
                    AddTargetsInDirectionIfExists(result, battle, grid, caster, position, down);
                    AddTargetsInDirectionIfExists(result, battle, grid, caster, position, left);
                    AddTargetsInDirectionIfExists(result, battle, grid, caster, position, right);
                    break;
            }

            return result;
        }

        public bool HasTargetInRange(BattleManager battle, BattleGrid grid, BattleCharacter caster)
        {
            var targetsDict = GetAllTargetSpaces(battle, grid, caster, caster.currPosition);
            return targetsDict.Count > 0;
        }

        private void AddTargetsInDirectionIfExists(Dictionary<CharacterDirection, Array<GridSpace>> spaces,
                                                   BattleManager battle, 
                                                   BattleGrid grid, 
                                                   BattleCharacter caster, 
                                                   Vector2I position,
                                                   CharacterDirection direction)
        {
            var results = GetSpacesInDirection(grid, caster, position, direction);
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
                                                    Vector2I position,
                                                    CharacterDirection direction)
        {
            Array<GridSpace> result = new Array<GridSpace>();
            var attackPositions = AttackArea.GetPositionsInRange(position, direction);
            foreach (var currPosition in attackPositions)
            {
                var space = grid.GetSpaceFromCoords(currPosition);
                if(space != null)
                {
                    result.Add(space);
                }
            }

            return result;
        }
    }
}