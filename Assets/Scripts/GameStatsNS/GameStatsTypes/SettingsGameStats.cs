using Localization.Scripts;
using MonoBehaviours.Color_Themes;
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
        public bool FitInSafeArea { get; set; } = true;
        public Theme Theme { get; set; } = Theme.RedDark;
    }
}