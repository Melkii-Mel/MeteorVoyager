using static MeteorVoyager.Assets.Scripts.GameStatsNameSpace.GameStats;
using MeteorVoyager.Assets.Scripts.GameStatsNameSpace;
using UnityEngine;
using UnityEngine.UI;

namespace MeteorVoyager.Assets.Scripts.MonoBehaviours
{
    public class Settings : MonoBehaviour
    {

        [SerializeField] GameObject settingsScreen;
        [SerializeField] Slider musicVolume;
        [SerializeField] Slider soundsVolume;
        [SerializeField] Slider starsAmount;
        [SerializeField] Toggle trails;
        [SerializeField] Toggle particles;
        [SerializeField] Button close;
        [SerializeField] StarsGenerator starsGenerator;

        public void Start()
        {
            OpenSettings();
            CloseSettings();
        }

        public void OpenSettings()
        {
            if (GameStats.IsSomeFieldEnabled) return;

            settingsScreen.SetActive(true);
            LoadValues();
            GameStats.IsSomeFieldEnabled = true;
        }
        public void CloseSettings()
        {
            settingsScreen.SetActive(false);
            GameStats.IsSomeFieldEnabled = false;
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