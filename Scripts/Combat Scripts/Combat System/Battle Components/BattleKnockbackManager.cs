﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Schema;
using Godot;

namespace CombatSystem
{
    public partial class BattleKnockbackManager : Node
    {
        public List<KnockbackData> Knockbacks { get; private set; } = new List<KnockbackData> ();
        private List<KnockbackData> completedKnockbacks = new List<KnockbackData> ();
        private List<KnockbackPositionData> prevPositions = new List<KnockbackPositionData> ();
        private List<KnockbackPositionData> newPositions = new List<KnockbackPositionData> ();
        private List<KnockbackCollisionData> collisions = new List<KnockbackCollisionData> ();

        private Dictionary<BattleCharacter, BattleCharacter> dependencies = new Dictionary<BattleCharacter, BattleCharacter>();

        private SimpleWeakRef<BattleManager> _battle;
        private BattleManager battle
        {
            get
            {
                return _battle.Value;
            }
            set
            {
                _battle = new SimpleWeakRef<BattleManager> (value);
            }
        }


        

        /***************
         * Godot Functions
         * **************/


        public override void _Ready()
        {
            battle = Utils.FindParentOfType<BattleManager>(this);
        }


        







        /******************
         * Add Knockback Data
         * *****************/

        /// <summary>
        /// Prepares the character to get knocked back
        /// </summary>
        /// <param name="character"></param>
        /// <param name="spaces"></param>
        /// <param name="direction"></param>
        public void ApplyKnockback(BattleCharacter character, int spaces, CharacterDirection direction)
        {
            if (!HasExistingKnockback(character))
            {
                var knockback = new KnockbackData(character, spaces, direction);
                Knockbacks.Add(knockback);
            }
            else
            {
                UpdateExistingKnockback(character, spaces, direction);
            }
        }


        /*************************
         * Run Knockback
         * ***********************/

        public List<KnockbackCollisionData> RunKnockbacks()
        {
            SetupDependencies();
            SortKnockbacksByDependencies();
            RemoveKnockedUnitsFromField();
            InitPositionsList();

            int currKnockback = 0;
            while (Knockbacks.Count > 0)
            {
                RunSingleKnockback(currKnockback);
                CheckForCollisions();
                UpdatePreviousPositions();
                currKnockback++;
            }

            UpdateTargetPositions();
            var result = new List<KnockbackCollisionData>();
            result.AddRange(collisions);
            ResetKnockbacks();
            return result;
        }

        private void RemoveKnockedUnitsFromField()
        {
            foreach(var kb in Knockbacks)
            {
                var character = kb.KnockedCharacter;
                battle.Grid.UnoccupySpace(character);
            }
        }

        private void InitPositionsList()
        {
            foreach (var knockback in Knockbacks)
            {
                var character = knockback.KnockedCharacter;
                var currPos = character.currPosition;
                int spaces = knockback.SpacesKnockedBack;
                var direction = knockback.KnockedDirection;
                var positionData = new KnockbackPositionData(character, currPos, spaces, direction);
                prevPositions.Add(positionData);
                newPositions.Add(positionData);
            }
        }

        private void SetupDependencies()
        {
            dependencies.Clear();
            foreach (var knockback in Knockbacks)
            {
                var character = knockback.KnockedCharacter;
                dependencies[character] = null;
            }

            foreach (var knockback in Knockbacks)
            {
                var direction = knockback.KnockedDirection;
                var character = knockback.KnockedCharacter;
                var coords = character.currPosition;
                var directionVector = BattleConstants.GetDirectionOffset(direction);

                int index = 1;
                var newCoords = coords + directionVector * index;
                while (battle.Grid.CoordinatesInGrid(newCoords))
                {
                    var space = battle.Grid.GetSpaceFromCoords(newCoords);
                    var charOnSpace = space.CharacterOnSpace;
                    if (charOnSpace != null && dependencies.ContainsKey(charOnSpace))
                    {
                        dependencies[character] = charOnSpace;
                        break;
                    }
                    index++;
                    newCoords = coords + directionVector * index;
                }
            }
        }

        private void RunSingleKnockback(int currKnockback)
        {
            int index = 0;
            while(index < Knockbacks.Count)
            {
                var knockback = Knockbacks[index];
                var directionVec = BattleConstants.GetDirectionOffset(knockback.KnockedDirection);
                int spaces = knockback.SpacesKnockedBack;
                if(currKnockback < spaces)
                {
                    var character = knockback.KnockedCharacter;
                    var direction = knockback.KnockedDirection;
                    int remainingSpaces = spaces - currKnockback;

                    UpdateCharacterPosition(character, direction, remainingSpaces);
                    index++;
                }
                else
                {
                    RemoveKnockbackAtIndex(index);
                }
            }
        }

        private void UpdateCharacterPosition(BattleCharacter character, CharacterDirection direction, int remainingSpaces)
        {
            for(int i = 0; i < newPositions.Count; i++)
            {
                var currPos = newPositions[i];
                if(currPos.Character == character)
                {
                    var offset = BattleConstants.GetDirectionOffset(direction);
                    var currCoords = currPos.Coords;
                    var newPos = currCoords + offset;
                    bool coordInRange = battle.Grid.CoordinatesInGrid(newPos);

                    if(coordInRange)
                        newPositions[i] = new KnockbackPositionData(character, newPos, remainingSpaces, direction);
                    else
                        newPositions[i] = new KnockbackPositionData(character, currCoords, remainingSpaces, direction);
                    break;
                }
            }
        }

        /// <summary>
        /// Checks for any collisions and removes all pending knockbacks that have
        /// resulted in a collision
        /// </summary>
        private void CheckForCollisions()
        {
            int index = 0;

            while(index < Knockbacks.Count)
            {
                var knockback = Knockbacks[index];
                KnockbackCollisionData collision = null;
                BattleCharacter character = knockback.KnockedCharacter;

                for(int j = 0; j < newPositions.Count; j++)
                {
                    var posData = newPositions[j];
                    if(posData.Character == knockback.KnockedCharacter)
                    {
                        collision = posData.CheckCollisionOnGrid(battle.Grid, dependencies);
                        if(collision == null) collision = posData.CheckCollisionWithKnockedUnits(newPositions, dependencies);
                        if(collision != null)
                        {
                            collisions.Add(collision);
                            RemoveKnockbackAtIndex(index);
                            newPositions[j] = prevPositions[j];
                            break;
                        }
                    }
                }
                if (collision == null) index++;
            }
        }

        private void SortKnockbacksByDependencies()
        {
            var dependenciesList = GetKnockedUnitsByDependencies();
            Knockbacks = Knockbacks.OrderBy(x => dependenciesList.IndexOf(x.KnockedCharacter)).ToList();
        }

        private List<BattleCharacter> GetKnockedUnitsByDependencies()
        {
            var currList = new List<BattleCharacter>();
            currList.AddRange(dependencies.Keys);
            var result = new List<BattleCharacter>();

            MoveNonDependencies(currList, result);
            AddRemainingUnits(currList, result);

            return result;
        }

        private void MoveNonDependencies(List<BattleCharacter> listA, List<BattleCharacter> listB)
        {
            int index = 0;
            while(index < listA.Count)
            {
                var character = listA[index];
                if (dependencies[character] == null)
                {
                    listA.RemoveAt(index);
                    listB.Add(character);
                }
                else
                {
                    index++;
                }
            }
        }
        
        private void AddRemainingUnits(List<BattleCharacter> listA, List<BattleCharacter> listB)
        {
            int index = 0;
            while(listA.Count > 0)
            {
                var character = listA[index];
                var currDependency = dependencies[character];
                if (listB.Contains(currDependency))
                {
                    listB.Add(character);
                    listA.RemoveAt(index);
                    if(listA.Count > 0) index = index % listA.Count;
                }
                else
                {
                    index = (index + 1) % listA.Count;
                }
            }
        }

        private void RemoveKnockbackAtIndex(int index)
        {
            var knockback = Knockbacks[index];
            Knockbacks.RemoveAt(index);
        }

        private void UpdatePreviousPositions()
        {
            for(int i = 0; i < prevPositions.Count; i++)
            {
                prevPositions[i] = newPositions[i];
            }
        }

        private void UpdateTargetPositions()
        {
            foreach(var pos in newPositions)
            {
                var character = pos.Character;
                var coords = pos.Coords;
                battle.OccupySpace(coords, character);
            }
        }


        /// <summary>
        /// Replaces a pre-existing knockback with a stronger one if one exists
        /// </summary>
        /// <param name="character"></param>
        /// <param name="spaces"></param>
        /// <param name="direction"></param>
        private void UpdateExistingKnockback(BattleCharacter character, int spaces, CharacterDirection direction)
        {
            for (int i = 0; i < Knockbacks.Count; i++)
            {
                var knockback = Knockbacks[i];
                if(knockback.KnockedCharacter == character)
                {
                    if(knockback.SpacesKnockedBack <= spaces)
                    {
                        var newKnockback = new KnockbackData (character, spaces, direction);
                        Knockbacks[i] = newKnockback;
                    }
                    break;
                }
            }
        }


        private bool HasExistingKnockback(BattleCharacter character)
        {
            bool result = false;

            foreach (var knockback in Knockbacks)
            {
                if (knockback.KnockedCharacter == character)
                {
                    result = true;
                    break;
                }
            }

            return result;
        }

        private void ResetKnockbacks()
        {
            Knockbacks.Clear();
            dependencies.Clear();
            completedKnockbacks.Clear();
            prevPositions.Clear();
            newPositions.Clear();
            collisions.Clear();
        }
    }
}
