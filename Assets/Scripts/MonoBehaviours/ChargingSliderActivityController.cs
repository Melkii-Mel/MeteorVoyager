using UnityEngine;
using static GameStatsNS.GameStats;

namespace MonoBehaviours
{
    public class ChargingSliderActivityController : MonoBehaviour
    {
        [SerializeField] private GameObject slider;
        private void Awake()
        {
            SetChargingSliderActivity();
        }

        private void Update()
        {
            SetChargingSliderActivity();
            // ReSharper disable once ObjectCreationAsStatement
            // ReSharper disable once HeapView.ObjectAllocation.Evident
            new WaitForSeconds(1);
        }

        private void SetChargingSliderActivity()
        {
            slider.SetActive(MainGameStatsHolder.TurretUpgrades.ChargeAttack != 0);
        }
    }
}