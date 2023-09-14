using Localization.Scripts;
using MonoBehaviours.Color_Themes;
using SerializationLibrary;

namespace GameStatsNS.GameStatsTypes
{
    public class SettingsGameStats : Serializable<SettingsGameStats>
    {
        private int _musicDelayS = 200;
        private float _musicVolume = 1;
        public float SoundsVolume { get; set; } = 1;

        public float MusicVolume
        {
            get => _musicVolume;
            set
            {
                _musicVolume = value;
                OnMusicVolumeUpdate?.Invoke(value);
            }
        }

        public float StarsAmountMultiplier { get; set; } = 1;
        public bool TrailsEnabled { get; set; } = true;
        public bool ParticlesEnabled { get; set; } = true;
        public Language Language { get; set; } = Language.En;
        public bool FitInSafeArea { get; set; } = true;
        public Theme Theme { get; set; } = Theme.RedDark;
        public int FrameRate { get; set; } = 30;
        public float ScreenShake { get; set; } = 1;

        public int MusicDelayS
        {
            get => _musicDelayS;
            set
            {
                _musicDelayS = value;
                OnDelayUpdate?.Invoke(value);
            }
                
        }


        #region events

        public delegate void DelayUpdateEventHandler(int newValue);
        public event DelayUpdateEventHandler OnDelayUpdate;

        public delegate void MusicVolumeUpdateEventHandler(float newValue);
        public event MusicVolumeUpdateEventHandler OnMusicVolumeUpdate;

        #endregion
    }
}