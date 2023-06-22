using static MeteorVoyager.Assets.Scripts.GameStatsNameSpace.GameStats;
using UnityEngine;

namespace MeteorVoyager.Assets.Scripts.MonoBehaviours
{
    public class ChargingSliderActivityController : MonoBehaviour
    {
        [SerializeField] private GameObject slider;
        private void Awake()
        {
            SetChargingSliderActivity();
        }
        void Update()
        {
            SetChargingSliderActivity();
            new WaitForSeconds(1);
        }

        void SetChargingSliderActivity()
        {
            if (MainGameStatsHolder.TurretUpgrades.ChargeAttack == 0)
            {
                slider.SetActive(false);
            }
            else
            {
                slider.SetActive(true);
            }
        }
    }
}