using MeteorVoyager.Assets.Scripts.GameStatsNameSpace;
using UnityEngine;

namespace MeteorVoyager.Assets.Scripts.MonoBehaviours
{
    class UltracoinSpawner : MonoBehaviour
    {
        [SerializeField] GameObject ultracoin;

        void Start ()
        {
            Enemy.OnAnyEnemyDestroyOrEnoughHits += SpawnUltracoin;
        }

        void SpawnUltracoin(Enemy enemy)
        {
            if (Calculator.RandomBool(Mathf.Sqrt(GameStats.MainGameStatsHolder.DataUpgrades.UltracoinSpawnChanceLvl) / 10))
            {
                Instantiate(ultracoin, enemy.transform.position, new Quaternion());
                return;
            }
        }
    }
}
