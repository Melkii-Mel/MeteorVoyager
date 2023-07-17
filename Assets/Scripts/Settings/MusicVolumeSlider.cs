using static MeteorVoyager.Assets.Scripts.GameStatsNameSpace.GameStats;
using UnityEngine;
using UnityEngine.UI;

namespace MeteorVoyager
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
