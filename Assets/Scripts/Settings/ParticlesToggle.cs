using UnityEngine;
using UnityEngine.UI;
using static GameStatsNS.GameStats;

namespace Settings
{
    public class ParticlesToggle : MonoBehaviour
    {
        private void Awake()
        {
            GetComponent<Toggle>().isOn = MainGameStatsHolder.Settings.ParticlesEnabled;
        }
        public void OnUpdate(bool value)
        {
            MainGameStatsHolder.Settings.ParticlesEnabled = value;
        }
    }
}
