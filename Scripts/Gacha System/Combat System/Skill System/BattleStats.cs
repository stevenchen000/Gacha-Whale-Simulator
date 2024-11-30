using Godot;
using System;

namespace CombatSystem
{
    public class BattleStats
    {
        public int currentHealth;
        public int maxHealth;
        public int attack;
        public int defense;

        public BattleStats(StatContainer stats)
        {
            currentHealth = stats.currentHealth;
            maxHealth = stats.maxHealth;
            attack = stats.attack;
            defense = stats.defense;
        }

        public void TakeDamage(BattleStats enemyStats, int skillPotency)
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