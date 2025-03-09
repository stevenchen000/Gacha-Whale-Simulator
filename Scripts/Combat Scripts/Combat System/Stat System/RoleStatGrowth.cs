using System;
using Godot;

namespace CombatSystem
{
    [GlobalClass]
    public partial class RoleStatGrowth : Resource
    {
        [Export] public int BaseMaxHealth { get; private set; }
        [Export] public float HealthPerLevel { get; private set; }
        
        [Export] public int BaseAttack { get; private set; }
        [Export] public float AttackPerLevel { get; private set; }
        
        [Export] public int BaseDefense { get; private set; }
        [Export] public float DefensePerLevel { get; private set; }
        
        [Export] public int BaseSpeed { get; private set; }
        [Export] public float SpeedPerLevel { get; private set; }

        
        public int GetMaxHealth(int level)
        {
            return (int)(BaseMaxHealth + (level-1) * HealthPerLevel);
        }

        public int GetAttack(int level)
        {
            return (int)(BaseAttack + (level - 1) * AttackPerLevel);
        }

        public int GetDefense(int level)
        {
            return (int)(BaseDefense + (level - 1) * DefensePerLevel);
        }

        public int GetSpeed(int level)
        {
            return (int)(BaseSpeed + (level - 1) * SpeedPerLevel);
        }
    }
}
