using Godot;
using System;

namespace CombatSystem
{
    [GlobalClass]
    public partial class KnockbackEffect : SkillEffect
    {
        [Export] private float duration = 0.5f;
        [Export] private bool knockInSkillDirection = true;
        [Export] private int numofSpaces = 1;
        

        protected override void _StartEffect(TurnData data)
        {
            ApplyKnockback(data);
        }

        protected override bool _RunEffect(TurnData data, TimeHandler timer)
        {
            return true;
        }

        protected override void _EndEffect(TurnData data)
        {
            
        }

        private void ApplyKnockback(TurnData data)
        {
            var targetSelection = data.targetSelection;

            if (targetSelection == null && targetSelection.Style != TargetSelectionStyle.SelectDirection) return;
            var caster = data.caster;
            foreach(var target in data.targets)
            {
                //MoveCharacter(data, caster, target);
                data.Battle.ApplyKnockback(target, numofSpaces, data.targetSelection.SelectedDirection);
            }
        }
        /*
        private void MoveCharacter(TurnData data, BattleCharacter caster, BattleCharacter target)
        {
            var direction = data.targetSelection.SelectedDirection;
            if (!knockInSkillDirection) direction = GetOppositeDirection(direction);

            var offset = GetOffsetFromDirection(direction);
            
            for (int i = numofSpaces; i > 0; i--)
            {
                bool knocked = _MoveCharacterInDirection(target, offset);
                if (!knocked)
                {
                    int damage = data.totalHpDamageDealt * i / 10;
                    target.Stats.TakeDamage(damage);
                    DamageNumberManager.ShowDamageNumber(target, damage, DamageType.HealthDamage);
                    break;
                }
            }
        }

        private bool _MoveCharacterInDirection(BattleCharacter target, Vector2I offset)
        {
            var newPos = target.turnStartPosition + offset;
            return target.MoveAndUpdate(newPos);
        }


        private CharacterDirection GetOppositeDirection(CharacterDirection direction)
        {
            CharacterDirection result = direction;

            switch (direction)
            {
                case CharacterDirection.UP:
                    result = CharacterDirection.DOWN;
                    break;
                case CharacterDirection.DOWN:
                    result = CharacterDirection.UP;
                    break;
                case CharacterDirection.LEFT:
                    result = CharacterDirection.RIGHT;
                    break;
                case CharacterDirection.RIGHT:
                    result = CharacterDirection.LEFT;
                    break;
            }

            return direction;
        }

        private Vector2I GetOffsetFromDirection(CharacterDirection direction)
        {
            Vector2I offset = Vector2I.Zero;

            switch (direction)
            {
                case CharacterDirection.UP:
                    offset = Vector2I.Down;
                    break;
                case CharacterDirection.DOWN:
                    offset = Vector2I.Up;
                    break;
                case CharacterDirection.LEFT:
                    offset = Vector2I.Left;
                    break;
                case CharacterDirection.RIGHT:
                    offset = Vector2I.Right;
                    break;
            }

            return offset;
        }*/
    }
}
