﻿using System;
using Godot;
using GachaSystem;
using Godot.Collections;
using CharacterCreator;
using SkillSystem;

namespace CombatSystem
{
    public partial class BattleCharacter : Node2D
    {
        private WeakReference<BattleManager> _battle;
        private BattleManager battle
        {
            get
            {
                BattleManager result = null;
                _battle.TryGetTarget(out result);
                return result;
            }
            set
            {
                _battle = new WeakReference<BattleManager>(value);
            }
        }
        private SimpleWeakRef<BattleParty> _party { get; set; }
        public BattleParty Party 
        { 
            get 
            { 
                return _party.Value; 
            }
            private set
            {
                _party = new SimpleWeakRef<BattleParty> (value);
            }
        }

        //Character data
        public CharacterData Character { get; private set; }
        [Export] private CharacterPortraitDisplay portrait;


        //Movement Vars
        [Export] private Node2D characterPos;
        [Export] public Node2D PositionFollower { get; private set; }
        public Vector2I turnStartPosition { get; private set; } = Vector2I.MinValue;
        public Vector2I currPosition { get; private set; } = Vector2I.MinValue;
        [Export] public float speed = 5;
        private MovementData moveData;
        public Array<Vector2I> WalkableSpaces 
        { 
            get
            {
                return moveData.WalkableSpaces;
            }
        }


        //Combat Vars
        [ExportCategory("Combat Vars")]
        [Export] public SkillManager Skills { get; private set; }
        [Export] public BattleStatsManager Stats { get; private set; }
        public Element CharacterElement { get { return Character.Character.Element; } }
        [Export] public EffectManager Status { get; private set; }
        [Export] public BattleCharacterFlags Flags { get; private set; }
        [Export] public CharacterAI AI { get; private set; }
        public double NextTurnTime {  get; private set; }
        public int TurnPriority { get; private set; } = 0;
        //Used for calculating turn count without free turns
        public int TurnsTaken { get; private set; }
        //Counts free turns, used for current turn damage counting
        public int ActionsTaken { get; private set; }
        public bool IsTakingTurn { get { return battle.GetCurrentCharacter() == this; } }
        public BattleCharacter TauntTarget { get; private set; }



        //Animations
        [ExportCategory("Animations")]
        [Export] private AnimationPlayer anim;
        [Export] private SkillCaster castManager;
        public bool IsCasting { get { return castManager.IsCasting; } }

        [ExportCategory("Audio")]
        [Export] private PackedScene hurtSound;
        [Export] private PackedScene healSound;



        public Array<BattleCharacter> targets { get; private set; }
        public int PartyIndex { get; private set; }



        public override void _Ready()
        {
            battle = Utils.FindParentOfType<BattleManager>(this);
        }

        public override void _Process(double delta)
        {
            
        }


        public void InitCharacter(CharacterData character, BattleParty party)
        {
            Character = character;
            Party = party;
            Stats._Init(character);
            Skills.Init(character);
            CharacterPortrait charPortrait = character.GetPortrait();
            portrait.UpdatePortrait(charPortrait);

            AI = character.Character.EnemyAI;
        }

        /********************
         * Position
         * ****************/

        public void RemoveFromField()
        {
            battle.GetGrid().UnoccupySpace(this);
            PlayAnimation("die");
            //SetPosition(new Vector2(9999, 9999));
        }

        public void AddToField()
        {
            
        }


        /**************
         * Appearance
         * ************/

        public void ChangeBorder(PortraitBorder border)
        {
            portrait.SetBorder(border);
        }


        /*************
         * Turn Functions
         * ************/


        public void StartCharacterTurn(BattleState battleState)
        {
            var grid = battleState.Grid;
            var moveData = grid.GetAllWalkableAreas(this);
            grid.SetWalkableData(moveData);

            targets = new Array<BattleCharacter>();
            currPosition = turnStartPosition;
        }

        /// <summary>
        /// Run at the start of anyone's turn
        /// Runs after StartCharacterTurn
        /// </summary>
        /// <param name="battleState"></param>
        public void AnyCharacterTurnStart(BattleState battleState)
        {
            DecreaseUnbreakCounter();
        }


        public void EndTurn(double delta, BattleManager battle, BattleGrid grid)
        {
            turnStartPosition = currPosition;

            ActionsTaken++;
            TurnsTaken++;
        }

        public CombatActionData GetAutomaticAction()
        {
            CombatActionData result = null;

            if(AI != null)
                result = AI.CalculateAction(battle, this);

            return result;
        }

        public void SetTurnTime(double newTurnTime)
        {
            NextTurnTime = newTurnTime;
        }

        public void SetTurnPriority(int priority)
        {
            TurnPriority = priority;
        }

        public void Delay(int numOfTurns)
        {
            battle.State.TurnOrder.DelayCharacter(this, numOfTurns);
        }


        /*****************
         * Movement
         * **************/
        

        public void SetPosition(GridSpace space)
        {
            turnStartPosition = space.Coords;
            currPosition = space.Coords;
            characterPos.GlobalPosition = space.GlobalPosition;
        }


        public new void SetPosition(Vector2 newPos)
        {
            characterPos.GlobalPosition = newPos;
        }

        public new Vector2 GetPosition()
        {
            return characterPos.GlobalPosition;
        }

        /**************
         * Damage
         * ************/

        public void TakeAmpDamage(int damage, bool isCrit = false)
        {
            if (!IsBroken())
            {
                Stats.TakeAmpDamage(damage);
            }
            PlayAnimation(CharacterAnimationNames.HurtAnimation);
            _ShowDamageNumber(damage, isCrit);
            _PlaySound(hurtSound);
        }

        private void _PlaySound(PackedScene audioScene)
        {
            var audio = Utils.InstantiateCopy(audioScene);
            AddChild(audio);
        }

        private void _ShowDamageNumber(int damage, bool isCrit)
        {
            if (!isCrit)
                DamageNumberManager.ShowDamageNumber(this, damage, DamageType.AmpDamage);
            else
                DamageNumberManager.ShowDamageNumber(this, damage, DamageType.CritDamage);
        }

        /// <summary>
        /// Adds amp to the character through damage or break bonus
        /// </summary>
        /// <param name="amount"></param>
        public void AddAmp(int amount)
        {
            Stats.AddAmpAmount(amount);
            UpdateBreakStatus();
        }

        /// <summary>
        /// Adds amp to the character through battery skills
        /// </summary>
        /// <param name="amount"></param>
        public void BatteryAmp(int amount)
        {
            Stats.AddAmpAmount(amount);
            UpdateBreakStatus();
            _PlaySound(healSound);
        }

        public void TakeHpDamage(int damage)
        {
            Stats.TakeDamage(damage);
            DamageNumberManager.ShowDamageNumber(this, damage, DamageType.HealthDamage);
        }


        /// <summary>
        /// Attempts to break the character
        /// If character gets broken, returns true
        /// Does not return true if character was still broken
        /// </summary>
        /// <returns></returns>
        public bool BreakCharacter()
        {
            bool result = false;

            if (IsBroken())
            {
                result = false;
            }
            else
            {
                //Will need to check if unbreakable here
                _InflictBreak();
                result = true;
            }

            return result;
        }

        private void _InflictBreak()
        {
            Flags.AddFlag(BattleFlagNames.breakFlagName);
            Flags.SetFlagValue(BattleFlagNames.turnsToUnbreakFlag, 5);
            Stats.SetSlidingStat(StatNames.Amp, 0);
            Delay(1);
        }



        public void UnbreakCharacter()
        {
            Flags.SetFlagValue(BattleFlagNames.breakFlagName, 0);
            if (IsCurrAmpBelowDefault())
            {
                SetCurrAmpToDefault();
            }
        }

        public bool IsBroken()
        {
            return Flags.GetFlag(BattleFlagNames.breakFlagName);
        }

        public int GetTurnsToUnbreak()
        {
            return Flags.GetFlagAmount(BattleFlagNames.turnsToUnbreakFlag);
        }

        private void DecreaseUnbreakCounter()
        {
            if (IsBroken())
            {
                //Utils.Print(this, $"Decreasing {Character.Character.Name}'s break counter...");
                Flags.RemoveFlag(BattleFlagNames.turnsToUnbreakFlag);
                //Utils.Print(this, Flags.GetFlagAmount(BattleFlagNames.turnsToUnbreakFlag));
                if(GetTurnsToUnbreak() <= 0)
                {
                    UnbreakCharacter();
                    //Utils.Print(this, "Character unbroken");
                }
            }
        }

        public void UpdateBreakStatus()
        {
            DecreaseUnbreakCounter();
            int currAmp = Stats.GetSlidingStat(StatNames.Amp);
            int spirit = Stats.GetStat(StatNames.Spirit);

            if(IsBroken() && currAmp >= spirit)
            {
                //Utils.Print(this, "Unbroken! CurrAmp > Spirit!");
                UnbreakCharacter();
            }
        }

        public void SetCurrAmpToDefault()
        {
            int spirit = Stats.GetStat(StatNames.Spirit);
            Stats.SetSlidingStat(StatNames.Amp, spirit);
        }

        private bool IsCurrAmpBelowDefault()
        {
            int spirit = Stats.GetStat(StatNames.Spirit);
            int currAmp = Stats.GetSlidingStat(StatNames.Amp);
            return currAmp < spirit;
        }

        public void Taunt(BattleCharacter caster)
        {
            TauntTarget = caster;
            Flags.SetFlagValue(BattleFlagNames.isTaunted, true);
        }

        public void Untaunt()
        {
            TauntTarget = null;
            Flags.SetFlagValue(BattleFlagNames.isTaunted, false);
        }

        /***************
         * Knockback
         * *************/
        /*
        public bool IsKnockedBack()
        {
            return Flags.GetFlag(BattleFlagNames.knockbackFlag);
        }

        public void SetKnockbackFlag(bool value)
        {
            Flags.SetFlagValue(BattleFlagNames.knockbackFlag, value);
        }

        public CharacterDirection GetKnockbackDirection()
        {
            int value = Flags.GetFlagAmount(BattleFlagNames.knockbackDirectionFlag);
            return (CharacterDirection)value;
        }

        public void SetKnockbackDirection(CharacterDirection direction)
        {
            Flags.SetFlagValue(BattleFlagNames.knockbackDirectionFlag, (int)direction);
        }

        public int GetKnockbackAmount()
        {
            return Flags.GetFlagAmount(BattleFlagNames.knockbackSpacesFlag);
        }

        public void SetKnockbackAmount(int spaces)
        {
            Flags.SetFlagValue(BattleFlagNames.knockbackSpacesFlag, spaces);
        }*/

        public void Knockback(int spaces, CharacterDirection direction)
        {
            battle.ApplyKnockback(this, spaces, direction);
        }

        /*******************
         * Status Effects
         * ****************/

        public void AddStatus(BattleCharacter caster, StatusEffect effect)
        {
            Status.AddEffect(caster, effect, battle);
        }


        /****************
         * Animations
         * *************/

        public void PlayAnimation(string animation)
        {
            anim.Play(animation);
        }

        public void CastSkill(SkillContainer skill, TurnData data, SkillCastData skillCast)
        {
            if (skill != null)
            {
                skill.ConsumeSkillUse(1);
                castManager.CastSkill(skill, data, skillCast);
            }
        }


        /*************
         * Helpers
         * *************/

        public bool IsDead() { return Stats.IsDead(); }


        public bool IsEnemy(BattleCharacter target)
        {
            return target.Party != Party;
        }

        public bool MoveAndUpdate(Vector2I position)
        {
            currPosition = position;
            turnStartPosition = position;
            var space = battle.Grid.GetSpaceFromCoords(position);
            SetPosition(space);
            return true;
        }

        public void SetParty(BattleParty party)
        {
            Party = party;
        }

        public bool IsPlayer()
        {
            return battle.State.PlayerParty == Party;
        }

    }
}
