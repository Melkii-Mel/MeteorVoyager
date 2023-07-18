using UnityEngine;
using UnityEngine.UI;
using static GameStatsNS.GameStats;

namespace MonoBehaviours
{
    public class Settings : MonoBehaviour
    {

        [SerializeField] private GameObject settingsScreen;
        [SerializeField] private Slider musicVolume;
        [SerializeField] private Slider soundsVolume;
        [SerializeField] private Slider starsAmount;
        [SerializeField] private Toggle trails;
        [SerializeField] private Toggle particles;
        [SerializeField] private Button close;
        [SerializeField] private StarsGenerator starsGenerator;

        public void Start()
        {
            OpenSettings();
            CloseSettings();
        }

        public void OpenSettings()
        {
            if (IsSomeFieldEnabled) return;

            settingsScreen.SetActive(true);
            LoadValues();
            IsSomeFieldEnabled = true;
        }
        public void CloseSettings()
        {
            settingsScreen.SetActive(false);
            IsSomeFieldEnabled = false;
        }

        public void SetMusicVolume(float @in)
        {
            MainGameStatsHolder.Settings.MusicVolume = @in;
        }

        public void SetSoundsVolume(float @in)
        {
            MainGameStatsHolder.Settings.SoundsVolume = @in;
        }

        public void SetStarsAmount(float @in)
        {
            MainGameStatsHolder.Settings.StarsAmountMultiplier = @in;
            starsGenerator.UpdateCoefficient();
        }

        public void ToggleTrails(bool @in)
        {
            MainGameStatsHolder.Settings.TrailsEnabled = @in;
        }

        public void ToggleParticles(bool @in)
        {
            MainGameStatsHolder.Settings.ParticlesEnabled = @in;
        }
        private void LoadValues()
        {
            musicVolume.value = MainGameStatsHolder.Settings.MusicVolume;
            soundsVolume.value = MainGameStatsHolder.Settings.SoundsVolume;
            starsAmount.value = MainGameStatsHolder.Settings.StarsAmountMultiplier;
            trails.isOn = MainGameStatsHolder.Settings.TrailsEnabled;
            particles.isOn = MainGameStatsHolder.Settings.ParticlesEnabled;
        }
    }
}