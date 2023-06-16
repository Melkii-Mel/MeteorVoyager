using Assets.Scripts.GameStatsNameSpace;
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
            if (TurretUpgrades.Instance.ChargeAttack == 0)
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