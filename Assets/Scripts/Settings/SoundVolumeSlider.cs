using UnityEngine;
using static GameStatsNS.GameStats;
using UnityEngine.UI;

namespace Settings
{
    public class SoundVolumeSlider : MonoBehaviour
    {
        private void Start()
        {
            GetComponent<Slider>().value = MainGameStatsHolder.Settings.SoundsVolume;
        }
        public void OnUpdate(float value)
        {
            MainGameStatsHolder.Settings.SoundsVolume = value;
        }
    }
}
