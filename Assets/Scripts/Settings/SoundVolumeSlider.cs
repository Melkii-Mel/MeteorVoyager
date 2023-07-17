using static MeteorVoyager.Assets.Scripts.GameStatsNameSpace.GameStats;
using UnityEngine;
using UnityEngine.UI;

namespace MeteorVoyager
{
    public class SoundVolumeSlider : MonoBehaviour
    {
        void Start()
        {
            GetComponent<Slider>().value = MainGameStatsHolder.Settings.SoundsVolume;
        }
        public void OnUpdate(float value)
        {
            MainGameStatsHolder.Settings.SoundsVolume = value;
        }
    }
}
