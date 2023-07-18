using UnityEngine;
using UnityEngine.UI;
using static GameStatsNS.GameStats;

namespace Settings
{
    public class MusicVolumeSlider : MonoBehaviour
    {
        private void Start()
        {
            GetComponent<Slider>().value = MainGameStatsHolder.Settings.MusicVolume;
        }
        public void OnUpdate(float value)
        {
            MainGameStatsHolder.Settings.MusicVolume = value;
        }
    }
}
