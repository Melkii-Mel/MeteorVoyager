using Assets.Scripts.GameStatsNameSpace;
using System.Collections.Generic;
using UnityEngine;

namespace MeteorVoyager.Assets.Scripts.MonoBehaviours
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private GameObject _enemy;
        [SerializeField] private float _intervalCoeff;
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
        public static InfiniteInteger EnemyHealth { get; set; }
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
            enemys.transform.localScale *= Mathf.Sqrt(coeff);
            enemys.GetComponent<Enemy>().health = EnemyHealth * coeff;
            enemys.GetComponent<Enemy>().speed *= 1f / Mathf.Sqrt(coeff);
            enemys.GetComponent<TrailRenderer>().widthMultiplier *= Mathf.Sqrt(coeff);
            enemys.GetComponent<TrailRenderer>().time *= Mathf.Sqrt(coeff);
            enemys.GetComponent<Enemy>().isGlowing = Random.value < MeteorUpgrades.Instance.GlowingEnemiesSpawnRate * 0.001f;
        }
    }
}