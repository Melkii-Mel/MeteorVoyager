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
            Relocation.OnRelocationStart += (_, _) => OnDisable();
            Relocation.OnRelocationEnd += (_, _) => OnEnable();
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
            private float _lifeTimeSeconds = 0.6f;
            private InfiniteInteger _damage;
            private Vector3 _deltaSizeStarting;
            private const float STARTING_DELTA_SIZE_COEFFICIENT = 0.3f;
            private const float DELTA_SIZE_COEFFICIENT = 10;
            private void Start()
            {
                _damage = DamageCalculator.CalculateDefaultDamage() * 
                          InfiniteInteger.Pow(2, GameStats.MainGameStatsHolder.DataUpgrades.ScreenExplosionLvl / 10f);
                _deltaSizeStarting = transform.localScale * STARTING_DELTA_SIZE_COEFFICIENT;
            }

            private void Update()
            {
                var thisTransform = transform;
                var localScale = thisTransform.localScale;
                
                localScale += (localScale + _deltaSizeStarting) * (Time.deltaTime * DELTA_SIZE_COEFFICIENT);
                
                thisTransform.localScale = localScale;
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