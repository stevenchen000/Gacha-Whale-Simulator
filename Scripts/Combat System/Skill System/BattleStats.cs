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

        public void TakeDamage(int damage)
        {
            currentHealth -= damage;
            GD.Print($"Took {damage} damage");
            if (IsDead()) GD.Print("Target has died");
        }

        public bool IsDead()
        {
            return currentHealth <= 0;
        }
    }
}