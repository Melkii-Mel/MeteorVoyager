using System.Collections.Generic;
using System.Linq;
using GameStatsNS;
using UnityEngine;
using UnityEngine.Serialization;
using static GameStatsNS.GameStats;


namespace MonoBehaviours
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private GameObject enemy;
        [SerializeField][Range(0.1f, 100f)] private float sizeMultiplier;
        private float _interval = 1;
        public float TimerInterval
        {
            get => _interval;
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
        public static List<GameObject> Enemies { get; } = new();
        public static InfiniteInteger EnemyHealth { get; set; } = 1;
        private Timer _timer;

        private void Start()
        {
            _timer = new(intervalS: TimerInterval, @event: _ => SpawnEnemy(), enableOnStart: true);
        }
        private void Update()
        {
            TimerInterval = GameStats.Parameters.SpawnDelay;
        }
        public void StartEnemiesSpawning()
        {
            _timer.StartTimer();
        }

        public void StopEnemiesSpawning()
        {
            _timer.Stop();
        }

        private void SpawnEnemy()
        {
            const int maxEnemies = 100;
            if (Enemies.Count > maxEnemies)
            {
                Enemies.Remove(Enemies.First());
            }
            float coeff = Random.Range(0.5f, 5f);
            GameObject enemys = Instantiate(enemy);
            Enemies.Add(enemys);
            float pos = Random.Range(Consts.LBorder, Consts.RBorder);
            float z = enemys.transform.position.z;
            enemys.transform.position = new Vector3(pos, Consts.UBorder * 1.05f, z);
            enemys.transform.localScale *= Mathf.Sqrt(coeff) * sizeMultiplier;
            Enemy enemysEnemyComponent = enemys.GetComponent<Enemy>();
            enemysEnemyComponent.health = GameStats.Parameters.Health * coeff;
            enemysEnemyComponent.speed *= 1f / Mathf.Sqrt(coeff);
            enemys.GetComponent<Enemy>().isGlowing = Random.value < MainGameStatsHolder.MeteorUpgrades.GlowingEnemiesSpawnRate * 0.001f;
        }
    }
}