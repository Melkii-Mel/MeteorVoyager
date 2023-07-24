using System;
using GameStatsNS;
using MonoBehaviours.Interfaces;
using UnityEngine;
using Random = System.Random;

namespace MonoBehaviours
{
    public class ScreenExplosion : MonoBehaviour
    {
        [SerializeField] private GameObject objPrefab;
        private GameObject _objPrefabCopy;
        private const float MS_PER_TICK = 1000;
        private float _msBuffer;
        private readonly Random _random = new();
        private readonly Func<int, float> _chanceFormula = (x) => 1 / (Mathf.Log((x / 500f + 3), 3));

        private void OnEnable()
        {
            GlobalTimer.OnTick += ControlExplosion;
        }

        private void OnDisable()
        {
            GlobalTimer.OnTick -= ControlExplosion;
        }

        private void Start()
        {
            _objPrefabCopy = Instantiate(objPrefab);
            _objPrefabCopy.SetActive(false);
            _objPrefabCopy.AddComponent<ExplosionObj>();
        }

        private void ControlExplosion(float deltaTimeMS)
        {
            _msBuffer += deltaTimeMS;
            while (_msBuffer > MS_PER_TICK)
            {
                _msBuffer -= MS_PER_TICK;
                float v1 = (float)_random.NextDouble();
                float v2 = _chanceFormula(GameStats.MainGameStatsHolder.DataUpgrades.ScreenExplosionLvl);
                if (v1 > v2)
                {
                    Explode();
                }
            }
        }
        
        private void Explode()
        {
            Instantiate(_objPrefabCopy, gameObject.transform, false).SetActive(true);
        }
        
        [Serializable]
        private class ExplosionObj : MonoBehaviour
        {
            private float _lifeTimeSeconds = 0.3f;
            private InfiniteInteger _damage; 
            private void Start()
            {
                _damage = DamageCalculator.CalculateDefaultDamage() * 
                          InfiniteInteger.Pow(2, GameStats.MainGameStatsHolder.DataUpgrades.ScreenExplosionLvl / 10f);
            }

            private void Update()
            {
                transform.localScale *= 1.3f;
                _lifeTimeSeconds -= Time.deltaTime;
                if (_lifeTimeSeconds <= 0)
                {
                    Destroy(gameObject);
                }
            }

            private void OnTriggerEnter2D(Collider2D collision)
            {
                if (collision.TryGetComponent(out IDamageable enemy))
                {
                    enemy.TakeDamage(_damage);
                }
            }
        }
    }
}