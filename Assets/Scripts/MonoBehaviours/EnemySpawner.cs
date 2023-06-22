using static MeteorVoyager.Assets.Scripts.GameStatsNameSpace.GameStats;
using System.Collections.Generic;
using UnityEngine;

namespace MeteorVoyager.Assets.Scripts.MonoBehaviours
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private GameObject _enemy;
        [SerializeField] private float _intervalCoeff;
        [SerializeField][Range(0.1f, 100f)] private float _sizeMultiplier;
        public float IntervalCoeff
        {
            get
            {
                return _intervalCoeff;
            }
            set
            {
                _intervalCoeff *= value;
                if (_intervalCoeff < 1)
                {
                    _intervalCoeff = 1;
                }
                _timer.IntervalS = _intervalCoeff;
            }
        }
        public static List<GameObject> Enemies { get; set; } = new();
        public static InfiniteInteger EnemyHealth { get; set; } = 1;
        private Timer _timer;
        void Start()
        {
            _timer = new(intervalS: _intervalCoeff, action: SpawnEnemy, enableOnStart: true);
        }
        public void StartEnemiesSpawning()
        {
            _timer.StartTimer();
        }

        public void StopEnemiesSpawning()
        {
            _timer.Stop();
        }

        void SpawnEnemy()
        {
            GameObject enemys = Instantiate(_enemy);
            Enemies.Add(enemys);
            float coeff = Random.Range(0.5f, 5f);
            float pos = Random.Range(Consts.LBorder, Consts.RBorder);
            float z = enemys.transform.position.z;
            enemys.transform.position = new Vector3(pos, Consts.UBorder * 1.05f, z);
            enemys.transform.localScale *= Mathf.Sqrt(coeff) * _sizeMultiplier;
            Enemy enemysEnemyComponent = enemys.GetComponent<Enemy>();
            EnemyHealth = InfiniteInteger.Pow(2, MainGameStatsHolder.TurretUpgrades.SpawnCooldown);
            enemysEnemyComponent.health = EnemyHealth * coeff;
            enemysEnemyComponent.speed *= 1f / Mathf.Sqrt(coeff);
            enemys.GetComponent<TrailRenderer>().widthMultiplier *= Mathf.Sqrt(coeff);
            enemys.GetComponent<TrailRenderer>().time *= Mathf.Sqrt(coeff);
            enemys.GetComponent<Enemy>().isGlowing = Random.value < MainGameStatsHolder.MeteorUpgrades.GlowingEnemiesSpawnRate * 0.001f;
        }
    }
}