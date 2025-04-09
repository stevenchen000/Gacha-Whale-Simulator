using System;
using Godot;
using Godot.Collections;
using static Godot.HttpRequest;

namespace CombatSystem
{
    public class TargetingData
    {
        public TargetSelectionStyle SelectionStyle { get; private set; }

        //Caster data
        public BattleCharacter Caster { get; private set; }
        public SkillContainer Skill { get; private set; }

        //Targets
        public Dictionary<CharacterDirection, Array<Vector2I>> _directionDict { get; private set; }
        public Dictionary<Vector2I, Array<Vector2I>> _spaceDict { get; private set; }
        public Array<Vector2I> _spaces { get; private set; }


        public TargetingData(BattleCharacter caster, SkillContainer skill, Dictionary<CharacterDirection, Array<Vector2I>> targets)
        {
            Caster = caster;
            Skill = skill;
            SelectionStyle = TargetSelectionStyle.SelectDirection;
            _directionDict = targets;
        }


        public TargetingData(BattleCharacter caster, SkillContainer skill, Dictionary<Vector2I, Array<Vector2I>> targets)
        {
            Caster = caster;
            Skill = skill;
            _spaceDict = targets;
            SelectionStyle = TargetSelectionStyle.SelectSpace;
        }

        public TargetingData(BattleCharacter caster, SkillContainer skill, Array<Vector2I> targets)
        {
            Caster = caster;
            Skill = skill;
            SelectionStyle = TargetSelectionStyle.None;
            _spaces = targets;
        }

        
        public bool ValidTargetExists(BattleGrid grid)
        {
            bool result = false;
            switch (SelectionStyle)
            {
                case TargetSelectionStyle.SelectSpace:
                    var spaceKeys = _spaceDict.Keys;
                    foreach(var coords in spaceKeys)
                    {
                        var selection = new TargetingSelection(coords);
                        result = ValidTargetInSelection(selection, grid);
                        if (result) break;
                    }
                    break;
                case TargetSelectionStyle.SelectDirection:
                    var directionKeys = _directionDict.Keys;
                    foreach(var direction in directionKeys)
                    {
                        var selection = new TargetingSelection(direction);
                        result = ValidTargetInSelection(selection, grid);
                        if (result) break;
                    }
                    break;
                case TargetSelectionStyle.None:
                    var noneSelection = new TargetingSelection();
                    result = ValidTargetInSelection(noneSelection, grid);
                    break;
            }
            return result;
        }

        public bool ValidTargetInSelection(TargetingSelection selection, BattleGrid grid)
        {
            bool result = false;
            var selectedSpaces = GetSpacesInSelection(selection);
            result = HasValidTargetInArea(selectedSpaces, grid);

            return result;
        }

        public Array<BattleCharacter> GetTargets(TargetingSelection selection, BattleGrid grid)
        {
            Array<Vector2I> selectedSpaces = GetSpacesInSelection(selection);
            Array<BattleCharacter> targets = new Array<BattleCharacter>();

            foreach(var coords in selectedSpaces)
            {
                var space = grid.GetSpaceFromCoords(coords);
                if (space != null)
                {
                    var target = space.CharacterOnSpace;
                    if (IsValidTarget(target))
                        targets.Add(target);
                }
            }

            return targets;
        }

        public Array<Vector2I> GetSpacesInSelection(TargetingSelection selection)
        {
            Array<Vector2I> selectedSpaces = null;

            switch (SelectionStyle)
            {
                case TargetSelectionStyle.SelectDirection:
                    var direction = selection.SelectedDirection;
                    selectedSpaces = _directionDict[direction];
                    break;
                case TargetSelectionStyle.SelectSpace:
                    var coords = selection.SelectedSpace;
                    selectedSpaces = _spaceDict[coords];
                    break;
                case TargetSelectionStyle.None:
                    selectedSpaces = _spaces;
                    break;
            }

            return selectedSpaces;
        }

        /// <summary>
        /// Used for direction selection to remove pointless selections
        /// </summary>
        /// <param name="grid"></param>
        /// <returns></returns>
        public Array<CharacterDirection> GetValidDirections(BattleGrid grid)
        {
            var result = new Array<CharacterDirection>();

            var directions = _directionDict.Keys;
            foreach (var direction in directions)
            {
                var spaces = _directionDict[direction];
                bool hasTarget = HasValidTargetInArea(spaces, grid);
                if (hasTarget)
                {
                    result.Add(direction);
                }
            }

            return result;
        }

        /// <summary>
        /// Used for space selection for AI to remove pointless selections.
        /// Probably don't want to use for players, might look awkward to remove 
        /// selectable spaces
        /// </summary>
        /// <param name="grid"></param>
        /// <returns></returns>
        public Array<Vector2I> GetValidSpaces(BattleGrid grid)
        {
            var result = new Array<Vector2I>();
            var selectableSpaces = _spaceDict.Keys;
            foreach(var coords in selectableSpaces)
            {
                var spaces = _spaceDict[coords];
                bool hasTarget = HasValidTargetInArea(spaces, grid);
                if (hasTarget) result.Add(coords);
            }

            return result;
        }


        private bool HasValidTargetInArea(Array<Vector2I> area, BattleGrid grid)
        {
            var result = false;

            foreach(var coords in area)
            {
                var space = grid.GetSpaceFromCoords(coords);
                if(space == null) continue;
                var target = space.CharacterOnSpace;
                if(IsValidTarget(target))
                {
                    result = true;
                    break;
                }
            }

            return result;
        }



        private bool IsValidTarget(BattleCharacter target)
        {
            var targetType = Skill.Skill.targetType;
            bool result = false;

            if(target == null)
            {
                result = false;
            }else if (targetType == TargetType.Ally)
            {
                result = !Caster.IsEnemy(target);
            }
            else if (targetType == TargetType.Enemy)
            {
                result = Caster.IsEnemy(target);
            }else if(targetType == TargetType.Self)
            {
                result = Caster == target;
            }

            return result;
        }
    }
}
