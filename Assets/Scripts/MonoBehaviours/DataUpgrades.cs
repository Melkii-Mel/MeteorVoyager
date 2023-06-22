using static MeteorVoyager.Assets.Scripts.GameStatsNameSpace.GameStats;
using UnityEngine;

namespace MeteorVoyager.Assets.Scripts.MonoBehaviours
{
    public class DataUpgrades : MonoBehaviour
    {
        public GameObject bossSpawnChanceButton;
        public GameObject forceFieldButton;
        public GameObject ultracoinSpawnChanceButton;
        public GameObject screenExplosionButton;
        public GameObject chargedAttackMultishotButton;

        public void Buy()
        {
            if (gameObject == bossSpawnChanceButton)
            {
                if (MainGameStatsHolder.Currency.Data > int.MaxValue)
                {
                    MainGameStatsHolder.DataUpgrades.BossSpawnChanceLvl++;
                }
            }
            else if (gameObject == forceFieldButton)
            {

            }
            else if (gameObject == ultracoinSpawnChanceButton)
            {

            }
            else if (gameObject == screenExplosionButton)
            {

            }
            else if (gameObject == chargedAttackMultishotButton)
            {

            }
        }
    }
}