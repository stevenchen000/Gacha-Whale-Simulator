using Godot;
using Godot.Collections;
using System;

namespace CombatSystem
{
    [Tool]
    [GlobalClass]
    public partial class StageParty : Resource
    {
        [Export] public Array<CharacterData> Characters { get; private set; } = new();
        [Export] public int GloryLevel { get; private set; } = 1;
        private GloryStats gloryStats;

        public StageParty() { }

        public StageParty(PartySetup party, int offense, int defense, int support)
        {
            Characters.AddRange(party.Party);
            gloryStats = new GloryStats(offense, defense, support);
        }

        public StageParty(Array<CharacterData> party, int lvl)
        {
            Characters.AddRange(party);
            GloryLevel = lvl;
        }

        public GloryStats GetGloryStats()
        {
            if (gloryStats == null)
                return new GloryStats(GloryLevel);
            else
                return gloryStats;
        }

        public void SetGloryLevel(int level)
        {
            GloryLevel = level;
        }
    }
}