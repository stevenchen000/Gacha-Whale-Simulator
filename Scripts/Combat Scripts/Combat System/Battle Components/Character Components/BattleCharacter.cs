using System;
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
        public Vector2I turnStartPosition { get; private set; } = Vector2I.MinValue;
        public Vector2I currPosition { get; private set; } = Vector2I.MinValue;
        public CharacterDirection facingDirection { get; private set; }
        [Export] public float speed = 5;
        public Array<Vector2I> WalkableSpaces { get; private set; } = new Array<Vector2I>();


        //Combat Vars
        [Export] public SkillManager Skills { get; private set; }
        [Export] public BattleStatsManager Stats { get; private set; }
        [Export] public EffectManager Status { get; private set; }
        [Export] public BattleCharacterFlags Flags { get; private set; }
        [Export] public CombatAI AI { get; private set; }
        public double NextTurnTime {  get; private set; }
        public int TurnPriority { get; private set; } = 0;
        //Used for calculating turn count without free turns
        public int TurnsTaken { get; private set; }
        //Counts free turns, used for current turn damage counting
        public int ActionsTaken { get; private set; }



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
            SetPosition(new Vector2(9999, 9999));
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
            WalkableSpaces = grid.GetAllWalkableAreas(this);
            targets = new Array<BattleCharacter>();
            currPosition = turnStartPosition;

            DecreaseUnbreakCounter();
        }


        public void EndTurn(double delta, BattleManager battle, BattleGrid grid)
        {
            WalkableSpaces = null;
            turnStartPosition = currPosition;

            ActionsTaken++;
            TurnsTaken++;
            //Need to check if free turn happened
        }

        public CombatActionData GetAutomaticAction()
        {
            CombatActionData result = null;

            if(AI != null)
                result = AI.CalculateAction(battle);

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

        public void SetTemporaryPosition(GridSpace space)
        {
            currPosition = space.Coords;
            characterPos.GlobalPosition = space.GlobalPosition;
        }

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

            _ShowDamageNumber(damage, isCrit);
        }

        private void _ShowDamageNumber(int damage, bool isCrit)
        {
            if (!isCrit)
                DamageNumberManager.ShowDamageNumber(this, damage, DamageType.AmpDamage);
            else
                DamageNumberManager.ShowDamageNumber(this, damage, DamageType.CritDamage);
        }

        public void AddAmp(int amount)
        {
            Stats.AddAmpAmount(amount);
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
            Flags.SetFlagValue(BattleFlagNames.breakFlagName, 5);
            Stats.SetSlidingStat(StatNames.Amp, 0);
            Delay(1);
        }



        public void UnbreakCharacter()
        {
            Flags.SetFlagValue(BattleFlagNames.breakFlagName, 0);
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
                Flags.RemoveFlag(BattleFlagNames.turnsToUnbreakFlag);
                if(GetTurnsToUnbreak() <= 0)
                {
                    UnbreakCharacter();
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
                UnbreakCharacter();
            }
        }

        /*******************
         * Status Effects
         * ****************/

        public void AddStatus(BattleCharacter caster, StatusEffect effect)
        {
            Status.AddEffect(caster, effect);
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
            bool moved = battle.OccupySpace(position, this);
            if (moved)
            {
                currPosition = position;
                turnStartPosition = position;
            }

            return moved;
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
