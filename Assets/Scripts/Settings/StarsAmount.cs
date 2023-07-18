using UnityEngine;
using UnityEngine.UI;
using static GameStatsNS.GameStats;

namespace Settings
{
    public class StarsAmount : MonoBehaviour
    {
        private void Start()
        {
            GetComponent<Slider>().value = MainGameStatsHolder.Settings.StarsAmountMultiplier;
        }
        public void OnUpdate(float value)
        {
            MainGameStatsHolder.Settings.StarsAmountMultiplier = value;
        }
    }
}
