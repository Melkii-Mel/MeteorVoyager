using static MeteorVoyager.Assets.Scripts.GameStatsNameSpace.GameStats;
using UnityEngine;
using UnityEngine.UI;

namespace MeteorVoyager
{
    public class TrailsToggle : MonoBehaviour
    {
        void Start()
        {
            GetComponent<Toggle>().isOn = MainGameStatsHolder.Settings.TrailsEnabled;
        }
        public void OnUpdate(bool value)
        {
            MainGameStatsHolder.Settings.TrailsEnabled = value;
        }
    }
}
