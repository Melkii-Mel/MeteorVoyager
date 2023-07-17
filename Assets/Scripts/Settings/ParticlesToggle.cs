using static MeteorVoyager.Assets.Scripts.GameStatsNameSpace.GameStats;
using UnityEngine;
using UnityEngine.UI;

namespace MeteorVoyager
{
    public class ParticlesToggle : MonoBehaviour
    {
        void Start()
        {
            GetComponent<Toggle>().isOn = MainGameStatsHolder.Settings.ParticlesEnabled;
        }
        public void OnUpdate(bool value)
        {
            MainGameStatsHolder.Settings.ParticlesEnabled = value;
        }
    }
}
