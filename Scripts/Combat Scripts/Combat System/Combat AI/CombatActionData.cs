using System;
using Godot;

namespace CombatSystem
{
    public class CombatActionData
    {
        public Vector2I CoordsToGo { get; private set; }
        public CharacterSkill Skill { get; private set; }
        public CharacterDirection Direction { get; private set; }

        public CombatActionData(Vector2I coordsToGo, CharacterSkill skill, CharacterDirection direction)
        {
            CoordsToGo = coordsToGo;
            Skill = skill;
            Direction = direction;
        }
    }
}
