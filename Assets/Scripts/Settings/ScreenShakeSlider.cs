using System;
using System.Globalization;
using GameStatsNS;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Settings
{
    public class ScreenShakeSlider : MonoBehaviour
    {
        [SerializeField] private Slider slider;
        [SerializeField] private TextMeshProUGUI valueText;

        private float ScreenShake
        {
            get => GameStats.MainGameStatsHolder.Settings.ScreenShake;
            set => GameStats.MainGameStatsHolder.Settings.ScreenShake = value;
        }

        public void Awake()
        {
            valueText.text = ScreenShake.ToString(CultureInfo.InvariantCulture);
            slider.value = ScreenShake;
        }

        public void ChangeScreenShakeValue(float value)
        {
            float val = value;
            ScreenShake = val;
            valueText.text = Math.Round(val, 2).ToString(CultureInfo.InvariantCulture);
        }
    }
}
