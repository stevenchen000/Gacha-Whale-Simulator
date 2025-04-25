using System;
using Godot;

namespace CombatSystem
{
    public class CombatActionData
    {
        public Vector2I CoordsToGo { get; private set; }
        public SkillContainer Skill { get; private set; }
        public TargetingData TargetData { get; private set; }
        public TargetingSelection Selection { get; private set; }
        public int Priority { get; private set; } = 0;

        public CombatActionData(Vector2I coordsToGo, 
                                SkillContainer skill, 
                                TargetingData data, 
                                TargetingSelection selection, 
                                int priority)
        {
            CoordsToGo = coordsToGo;
            Skill = skill;
            TargetData = data;
            Selection = selection;
            Priority = priority;
        }
    }
}
