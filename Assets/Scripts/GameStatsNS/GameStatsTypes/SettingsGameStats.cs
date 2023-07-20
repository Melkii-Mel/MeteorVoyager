using Localization.Scripts;
using SerializationLibrary;

namespace GameStatsNS.GameStatsTypes
{
    public class SettingsGameStats : Serializable<SettingsGameStats>
    {
        public float SoundsVolume { get; set; } = 1;
        public float MusicVolume { get; set; } = 1;
        public float StarsAmountMultiplier { get; set; } = 1;
        public bool TrailsEnabled { get; set; } = true;
        public bool ParticlesEnabled { get; set; } = true;
        public Language Language { get; set; } = Language.En;
    }
}