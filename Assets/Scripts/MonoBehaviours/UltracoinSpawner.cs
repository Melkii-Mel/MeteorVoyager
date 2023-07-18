using GameStatsNS;
using UnityEngine;

namespace MonoBehaviours
{
    internal class UltracoinSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject ultracoin;

        private void Start ()
        {
            Enemy.OnAnyEnemyDestroyOrEnoughHits += SpawnUltracoin;
        }

        private void SpawnUltracoin(Enemy enemy)
        {
            if (Calculator.RandomBool(Mathf.Sqrt(GameStats.MainGameStatsHolder.DataUpgrades.UltracoinSpawnChanceLvl) / 10))
            {
                Instantiate(ultracoin, enemy.transform.position, new Quaternion());
                return;
            }
        }
    }
}
