using System;
using Godot;
using GachaSystem;
using Godot.Collections;

namespace CombatSystem
{
    public partial class DamageNumberManager : Node
    {
        [Export] private PackedScene scene;
        [Export] private Vector2 offset;
        [Export] private float randomness = 10;
        private static DamageNumberManager instance;

        public override void _Ready()
        {
            if(instance == null)
            {
                instance = this;
            }
            else
            {
                QueueFree();
            }
        }


        public static void ShowDamageNumber(BattleCharacter target, int damage, bool isCrit = false)
        {
            var offset = instance.offset;
            int randX = GameState.GetRandomNumber(0, (int)instance.randomness);
            int randY = GameState.GetRandomNumber(0, (int)instance.randomness);
            offset += new Vector2(randX, randY);

            var damageText = CreateCopy(damage);
            target.AddChild(damageText);
            damageText.Position = offset;
        }

        private static DamageNumber CreateCopy(int damage)
        {
            var result = (DamageNumber)Utils.InstantiateCopy(instance.scene);
            result.SetValue(damage);
            return result;
        }
    }
}
