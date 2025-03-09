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


        public void TakeDamage(int damage)
        {
            currentHealth -= damage;
            Utils.Print(this, $"Took {damage} damage");
            if (IsDead()) Utils.Print(this, "Target has died");
        }

        public bool IsDead()
        {
            return currentHealth <= 0;
        }
    }
}