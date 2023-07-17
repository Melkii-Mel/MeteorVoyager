using static MeteorVoyager.Assets.Scripts.GameStatsNameSpace.GameStats;
using UnityEngine;
using UnityEngine.UI;

namespace MeteorVoyager
{
    public class StarsAmount : MonoBehaviour
    {
        void Start()
        {
            GetComponent<Slider>().value = MainGameStatsHolder.Settings.StarsAmountMultiplier;
        }
        public void OnUpdate(float value)
        {
            MainGameStatsHolder.Settings.StarsAmountMultiplier = value;
        }
    }
}
