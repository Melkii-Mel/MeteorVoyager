using System;
using GameStatsNS;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Settings
{
    public class FPS : MonoBehaviour
    {
        [SerializeField] private Slider fps;
        [SerializeField] private TextMeshProUGUI fpsText;

        private int FrameRate
        {
            get => GameStats.MainGameStatsHolder.Settings.FrameRate;
            set => GameStats.MainGameStatsHolder.Settings.FrameRate = value;
        }

        public void Awake()
        {
            fpsText.text = FrameRate.ToString();
            fps.value = FrameRate;
        }

        public void ChangeFPS(float value)
        {
            int val = (int)value;
            FrameRate = val;
            fpsText.text = val.ToString();
        }
    }
}
