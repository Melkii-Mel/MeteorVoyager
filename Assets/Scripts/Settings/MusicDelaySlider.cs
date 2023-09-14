using GameStatsNS;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Settings
{
    public class MusicDelaySlider : MonoBehaviour
    {
        [SerializeField] private Slider musicDelaySlider;
        [SerializeField] private TextMeshProUGUI musicDelayValueText;

        private int DelayS
        {
            get => GameStats.MainGameStatsHolder.Settings.MusicDelayS;
            set => GameStats.MainGameStatsHolder.Settings.MusicDelayS = value;
        }
        private void Awake()
        {
            musicDelayValueText.text = DelayS.ToString();
            musicDelaySlider.value = DelayS;
        }

        public void OnUpdate(float value)
        {
            DelayS = (int)value;
            musicDelayValueText.text = DelayS.ToString();
        }
    }
}
