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
        public int speed;

        public BattleStats()
        {

        }

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
            currentHealth = Math.Max(currentHealth, 0);
        }

        public void HealHealth(int healing)
        {
            currentHealth += healing;
            currentHealth = Math.Min(currentHealth, maxHealth);
        }

        public bool IsDead()
        {
            return currentHealth <= 0;
        }
    }
}