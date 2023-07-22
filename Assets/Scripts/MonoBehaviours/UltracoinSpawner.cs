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
                Vector2 position = enemy.transform.position;
                position.y = Consts.UBorder;
                Instantiate(ultracoin, position, new Quaternion());
                return;
            }
        }
    }
}
