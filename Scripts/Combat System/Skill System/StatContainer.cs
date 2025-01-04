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
            GD.Print($"Took {damage} damage");
            if (IsDead()) GD.Print("Target has died");
        }

        public bool IsDead()
        {
            return currentHealth <= 0;
        }
    }
}