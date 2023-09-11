using System.Collections.Generic;
using System.Linq;
using GameStatsNS;
using UnityEngine;
using static GameStatsNS.GameStats;


namespace MonoBehaviours
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private GameObject enemy;
        [SerializeField][Range(0.1f, 100f)] private float sizeMultiplier;
        [SerializeField] private AsteroidsMaterialAndMeshRandomizer randomizer;
        [SerializeField] private float maxEnemies = 200;
        private float _interval = 1;
        private const float INTERVAL_MULTIPLIER = 0.5f;

        private float TimerInterval
        {
            get => _interval;
            set
            {
                _interval = value * INTERVAL_MULTIPLIER;
                if (_interval <= 0)
                {
                    _interval = 0.01f;
                }
                _timer.IntervalMS = _interval;
            }
        }
        public static List<GameObject> Enemies { get; } = new();
        public static InfiniteInteger EnemyHealth { get; set; } = 1;
        private Timer _timer;

        private void Start()
        {
            _timer = new(intervalMS: TimerInterval, @event: _ => SpawnEnemy(), enableOnStart: true);
        }
        private void Update()
        {
            TimerInterval = GameStats.Parameters.SpawnDelay * 1000;
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
            if (Enemies.Count > maxEnemies)
            {
                Enemies.Remove(Enemies.First());
            }
            float coeff = Random.Range(0.5f, 5f);
            GameObject enemys = Instantiate(enemy);
            Enemies.Add(enemys);
            float pos = Random.Range(Consts.LBorder, Consts.RBorder);
            float z = enemys.transform.position.z;
            
            Vector3 position = new Vector3(pos, Consts.UBorder * 1.05f, z);
            float scaleMultiplier = Mathf.Sqrt(coeff) * sizeMultiplier;
            InfiniteInteger health = GameStats.Parameters.Health * coeff;
            float speedMultiplier = 1f / Mathf.Sqrt(coeff);
            bool isGlowing = Random.value < MainGameStatsHolder.MeteorUpgrades.GlowingEnemiesSpawnRate * 0.001f;
            randomizer.SetMeshAndMaterial(enemys.GetComponentInChildren<MeshFilter>(), enemys.GetComponentInChildren<MeshRenderer>());
            
            enemys.GetComponent<Enemy>().Initialize(position, scaleMultiplier, health, speedMultiplier, isGlowing);
        }
    }
}