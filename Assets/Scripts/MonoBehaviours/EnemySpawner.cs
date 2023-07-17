using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static MeteorVoyager.Assets.Scripts.GameStatsNameSpace.GameStats;

namespace MeteorVoyager.Assets.Scripts.MonoBehaviours
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private GameObject _enemy;
        [SerializeField][Range(0.1f, 100f)] private float _sizeMultiplier;
        [SerializeField][Range(0.1f, 10f)] private float _intervalCoeffSF;
        private float _interval = 1;
        public float TimerInterval
        {
            get
            {
                return _interval;
            }
            set
            {
                _interval = value;
                if (_interval <= 0)
                {
                    _interval = 0.1f;
                }
                _timer.IntervalS = _interval;
            }
        }
        public static List<GameObject> Enemies { get; set; } = new();
        public static InfiniteInteger EnemyHealth { get; set; } = 1;
        private Timer _timer;
        void Start()
        {
            _timer = new(intervalS: TimerInterval, @event: SpawnEnemy, enableOnStart: true);
        }
        private void Update()
        {
            TimerInterval = _intervalCoeffSF * EnemyHealthAndSpawnDelayCoefficientsCalculator.CalculateSpawnDelayCoefficient();
            EnemyHealth = EnemyHealthAndSpawnDelayCoefficientsCalculator.CalculateHealthCoefficient();
        }
        public void StartEnemiesSpawning()
        {
            _timer.StartTimer();
        }

        public void StopEnemiesSpawning()
        {
            _timer.Stop();
        }

        void SpawnEnemy(float deltaTimeMS)
        {
            if (Enemies.Count > 100)
            {
                Enemies.Remove(Enemies.First());
            }
            float coeff = Random.Range(0.5f, 5f);
            GameObject enemys = Instantiate(_enemy);
            Enemies.Add(enemys);
            float pos = Random.Range(Consts.LBorder, Consts.RBorder);
            float z = enemys.transform.position.z;
            enemys.transform.position = new Vector3(pos, Consts.UBorder * 1.05f, z);
            enemys.transform.localScale *= Mathf.Sqrt(coeff) * _sizeMultiplier;
            Enemy enemysEnemyComponent = enemys.GetComponent<Enemy>();
            enemysEnemyComponent.health = EnemyHealthAndSpawnDelayCoefficientsCalculator.CalculateHealthCoefficient() * coeff;
            enemysEnemyComponent.speed *= 1f / Mathf.Sqrt(coeff);
            enemys.GetComponent<Enemy>().isGlowing = Random.value < MainGameStatsHolder.MeteorUpgrades.GlowingEnemiesSpawnRate * 0.001f;
        }
    }
}