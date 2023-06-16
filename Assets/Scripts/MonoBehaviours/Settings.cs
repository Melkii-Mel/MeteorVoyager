using Assets.Scripts.GameStatsNameSpace;
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
            SettingsGameStats.Instance.MusicVolume = @in;
        }

        public void SetSoundsVolume(float @in)
        {
            SettingsGameStats.Instance.SoundsVolume = @in;
        }

        public void SetStarsAmount(float @in)
        {
            SettingsGameStats.Instance.StarsAmountMultiplier = @in;
            starsGenerator.UpdateCoefficient();
        }

        public void ToggleTrails(bool @in)
        {
            SettingsGameStats.Instance.TrailsEnabled = @in;
        }

        public void ToggleParticles(bool @in)
        {
            SettingsGameStats.Instance.ParticlesEnabled = @in;
        }
        private void LoadValues()
        {
            musicVolume.value = SettingsGameStats.Instance.MusicVolume;
            soundsVolume.value = SettingsGameStats.Instance.SoundsVolume;
            starsAmount.value = SettingsGameStats.Instance.StarsAmountMultiplier;
            trails.isOn = SettingsGameStats.Instance.TrailsEnabled;
            particles.isOn = SettingsGameStats.Instance.ParticlesEnabled;
        }
    }
}