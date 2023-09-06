using System;
using Animations;
using GameStatsNS.GameStatsTypes;
using MonoBehaviours.Interfaces;
using UnityEngine;
using static GameStatsNS.GameStats;
using Random = UnityEngine.Random;

namespace MonoBehaviours
{
    public class Enemy : MonoBehaviour, IDamageable
    {
        private const int ENOUGH_HITS = 5;
        [SerializeField] private GameObject particles;
        private float _lifetime = 20;
        private Color _color;
        private Color _startingColor;
        public InfiniteInteger Health { get; private set; }
        public float Speed { get; private set; } = 2;
        public bool IsGlowing { get; private set; }
        public int Hits { get; private set; }

        #region events;

        public delegate void EnemyEventHandler(Enemy enemy);
        public event EnemyEventHandler OnEnemyDestroy;
        public static event EnemyEventHandler OnAnyEnemyDestroy;
        public event EnemyEventHandler OnEnemyDestroyOrEnoughHits;
        public static event EnemyEventHandler OnAnyEnemyDestroyOrEnoughHits;
        public event EnemyEventHandler OnEnemyDespawn;
        public static event EnemyEventHandler OnAnyEnemyDespawn;
        public event EnemyEventHandler OnDamageTaken;
        public static event EnemyEventHandler OnAnyEnemyDamageTaken;

        #endregion
        private void Start()
        {
            GetComponentInChildren<GlowingPulse>().enabled = IsGlowing;
            if (_initialized)
            {
                return;
            }

            try
            {
                throw new InvalidOperationException(
                    "Initialize method has not been called upon Instantiation of a game object with Enemy component");
            }
            finally
            {
                Destroy(this);
            }
        }

        private void Update()
        {
            transform.Translate(new Vector2(0, -3) * (Time.deltaTime * Speed * (IsGlowing ? 0.5f : 1)), Space.Self);
            _lifetime -= Time.deltaTime;
            if (_lifetime < 0)
            {
                OnEnemyDespawn?.Invoke(this);
                OnAnyEnemyDespawn?.Invoke(this);
                EnemySpawner.Enemies.Remove(gameObject);
                Destroy(gameObject);
            }
        }

        private bool _initialized = false;
        public void Initialize(Vector3 position, float scaleMultiplier, InfiniteInteger stHealth, float speedMultiplier, bool stIsGlowing)
        {
            if (_initialized)
            {
                throw new InvalidOperationException("Initialization should be called only once");
            }
            Transform thisTransform = transform;
            thisTransform.position = position;
            thisTransform.localScale *= scaleMultiplier;
            Quaternion newRot = thisTransform.rotation;
            thisTransform.Rotate(0, 0,Random.Range(-15, 15));
            Health = stHealth;
            Speed *= speedMultiplier;
            IsGlowing = stIsGlowing;
            _initialized = true;
        }

        private void Undo()
        {
            OnEnemyDestroy?.Invoke(this);
            OnAnyEnemyDestroy?.Invoke(this);
            if (Hits < ENOUGH_HITS)
            {
                OnEnemyDestroyOrEnoughHits?.Invoke(this);
                OnAnyEnemyDestroyOrEnoughHits?.Invoke(this);
                if (IsGlowing)
                {
                    GivePowerUp();
                }
            }
            EnemySpawner.Enemies.Remove(gameObject);
            Destroy(gameObject);
        }
        
        public void TakeDamage(InfiniteInteger damage, Quaternion direction)
        {
            OnDamageTaken?.Invoke(this);
            OnAnyEnemyDamageTaken?.Invoke(this);
            Hits++;
            if (Hits == ENOUGH_HITS)
            {
                OnEnemyDestroyOrEnoughHits?.Invoke(this);
                OnAnyEnemyDestroyOrEnoughHits?.Invoke(this);
            }
            if (IsGlowing && Hits == 5)
            {
                IsGlowing = false;
                transform.GetChild(0).gameObject.SetActive(IsGlowing);
                GivePowerUp();
            }
            if (MainGameStatsHolder.Settings.ParticlesEnabled)
            {
                var transform1 = transform;
                if (direction == default) direction = transform1.rotation;
                Instantiate(particles, transform1.position, direction);
            }
            if (Health <= 0)
            {
                return;
            }
            InfiniteInteger prevHealth = Health;
            Health -= damage;
            if (Health > 0)
            {
                transform.Translate(Time.deltaTime * Speed * (IsGlowing ? 0.5f : 1) * 10 * new Vector2(0, 3));
                GiveMatter(damage);
            }
            else
            {
                GiveMatter(prevHealth);
                Undo();
            }
        }
        public delegate InfiniteInteger MatterMultipliersHandler();

        public static event MatterMultipliersHandler MatterMultipliers;

        private static void GiveMatter(InfiniteInteger damage)
        {
            InfiniteInteger deltaMatter = damage;
            deltaMatter *= MainGameStatsHolder.Timers.CoinMultiplierTimer > 0 ? 3 : 1;
            deltaMatter *= MainGameStatsHolder.Timers.X10Reward > 0 ? 10 : 1;
            deltaMatter *= InfiniteInteger.Pow(2, MainGameStatsHolder.MeteorUpgrades.CoinMultiplier);
            if (MatterMultipliers != null)
            {
                foreach (var @delegate in MatterMultipliers.GetInvocationList())
                {
                    var multiplier = (Func<InfiniteInteger>)@delegate;
                    deltaMatter *= multiplier();
                }
            }

            MainGameStatsHolder.Currency.Balance += deltaMatter;
        }
        private void GivePowerUp()
        {
            const float coeff = 0.1f;
            switch (UnityEngine.Random.Range(0, 3))
            {
                case 0: MainGameStatsHolder.Timers.AddTime(MainGameStatsHolder.MeteorUpgrades.CoinMultiplierTimeUpgrade * coeff, Timers.Timer.CoinMultiplierTimer); break;
                case 1: MainGameStatsHolder.Timers.AddTime(MainGameStatsHolder.MeteorUpgrades.DamageMultiplierTimeUpgrade * coeff, Timers.Timer.DamageMultiplierTimer); break;
                case 2: MainGameStatsHolder.Timers.AddTime(MainGameStatsHolder.MeteorUpgrades.ExplosivesAttacksTimeUpgrade * coeff, Timers.Timer.ExplosivesAttacksTimer); break;
            }
        }
    }
}