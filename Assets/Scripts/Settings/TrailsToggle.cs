using UnityEngine;
using UnityEngine.UI;
using static GameStatsNS.GameStats;

namespace Settings
{
    public class TrailsToggle : MonoBehaviour
    {
        private void Start()
        {
            GetComponent<Toggle>().isOn = MainGameStatsHolder.Settings.TrailsEnabled;
        }
        public void OnUpdate(bool value)
        {
            MainGameStatsHolder.Settings.TrailsEnabled = value;
        }
    }
}
