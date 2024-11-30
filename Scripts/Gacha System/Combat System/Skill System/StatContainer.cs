using Godot;
using System;

namespace CombatSystem
{
    [GlobalClass]
    public partial class StatContainer : Resource
    {
        [Export] public int currentHealth { get; set; }
        [Export] public int maxHealth { get; set; }
        [Export] public int attack { get; set; }
        [Export] public int defense { get; set; }


        public void TakeDamage(StatContainer enemyStats, int skillPotency)
        {
            int enemyAttack = enemyStats.attack;
            int baseDamage = enemyAttack - defense / 2;
            int totalDamage = baseDamage * skillPotency / 100;

            currentHealth -= totalDamage;
            GD.Print($"Took {totalDamage} damage");
            if (IsDead()) GD.Print("Target has died");
        }

        public bool IsDead()
        {
            return currentHealth <= 0;
        }
    }
}