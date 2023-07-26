using GameStatsNS.GameStatsTypes;
using MonoBehaviours.Interfaces;
using Unity.Mathematics;
using UnityEngine;
using static GameStatsNS.GameStats;

namespace MonoBehaviours
{
    public class Enemy : MonoBehaviour, IDamageable
    {
        private const int ENOUGH_HITS = 5;
        [SerializeField] private GameObject particles;
        [SerializeField] private PowerUpController powerUpController;
        private float _lifetime = 20;
        private Color _color;
        private Color _startingColor;
        public InfiniteInteger health = 1;
        private InfiniteInteger _startingHealth;
        public float speed;
        public bool isGlowing;
        public int hits;

        #region events;

        public delegate void EnemyDestroyEventHandler(Enemy enemy);
        public event EnemyDestroyEventHandler OnEnemyDestroy;
        public static event EnemyDestroyEventHandler OnAnyEnemyDestroy;
        public event EnemyDestroyEventHandler OnEnemyDestroyOrEnoughHits;
        public static event EnemyDestroyEventHandler OnAnyEnemyDestroyOrEnoughHits;
        public event EnemyDestroyEventHandler OnEnemyDespawn;
        public static event EnemyDestroyEventHandler OnAnyEnemyDespawn;

        #endregion
        private void Start()
        {
            transform.GetChild(0).gameObject.SetActive(isGlowing);
            _startingHealth = health;
            SetStartingColor();
        }

        private void Update()
        {
            transform.Translate(new Vector2(0, -3) * (Time.deltaTime * speed * (isGlowing ? 0.5f : 1)));
            _lifetime -= Time.deltaTime;
            if (_lifetime < 0)
            {
                OnEnemyDespawn?.Invoke(this);
                OnAnyEnemyDespawn?.Invoke(this);
                EnemySpawner.Enemies.Remove(gameObject);
                Destroy(gameObject);
            }
        }

        public void Undo()
        {
            OnEnemyDestroy?.Invoke(this);
            OnAnyEnemyDestroy?.Invoke(this);
            if (hits < ENOUGH_HITS)
            {
                OnEnemyDestroyOrEnoughHits?.Invoke(this);
                OnAnyEnemyDestroyOrEnoughHits?.Invoke(this);
                if (isGlowing)
                {
                    GivePowerUp();
                }
            }
            if (MainGameStatsHolder.Settings.ParticlesEnabled)
            {
                try
                {
                    Transform transform1;
                    SpriteRenderer particle = Instantiate(particles, (transform1 = transform).position, transform1.rotation).GetComponent<SpriteRenderer>();
                    particle.color = _color;
                }
                catch
                {
                    // ignored
                }
            }
            EnemySpawner.Enemies.Remove(gameObject);
            Destroy(gameObject);
        }
        public void TakeDamage(InfiniteInteger damage)
        {
            hits++;
            if (hits == ENOUGH_HITS)
            {
                OnEnemyDestroyOrEnoughHits?.Invoke(this);
                OnAnyEnemyDestroyOrEnoughHits?.Invoke(this);
            }
            if (isGlowing && hits == 5)
            {
                isGlowing = false;
                transform.GetChild(0).gameObject.SetActive(isGlowing);
                GivePowerUp();
            }
            ChangeColor();
            if (MainGameStatsHolder.Settings.ParticlesEnabled)
            {
                var transform1 = transform;
                Instantiate(particles, transform1.position, transform1.rotation);
            }
            InfiniteInteger prevHealth = health;
            if (damage < 1)
            {
                damage = 1;
            }
            health -= damage;
            if (health > 0)
            {
                transform.Translate(Time.deltaTime * speed * (isGlowing ? 0.5f : 1) * 10 * new Vector2(0, 3));
                GiveMatter(damage);
            }
            else
            {
                GiveMatter(prevHealth);
                Undo();
            }
        }

        private const int MATTER_MULTIPLIER = 10;
        private static void GiveMatter(InfiniteInteger damage)
        {
            var balance = MainGameStatsHolder.Currency.Balance;
            if (balance <= 0)
            {
                MainGameStatsHolder.Currency.Balance = 0;
            }
            if (damage <= 0)
            {
                return;
            }
            InfiniteInteger reward = damage;
            reward *= MainGameStatsHolder.Timers.CoinMultiplierTimer > 0 ? 3 : 1;
            reward *= MainGameStatsHolder.Timers.X10Reward > 0 ? 10 : 1;
            reward *= (int)Mathf.Pow(2, MainGameStatsHolder.MeteorUpgrades.CoinMultiplier);
            reward *= MATTER_MULTIPLIER;
            MainGameStatsHolder.Currency.Balance += reward;
        }

        private float Sigmoid(float value, float displacement = 0)
        {
            value = 1f / (1f + Mathf.Pow(math.E, Mathf.Sqrt(value) - displacement));
            return Mathf.Pow(value, 0.25f);
        }

        private void ChangeColor()
        {
            Color currentColor = _startingColor;
            currentColor.r *= (float)(health / _startingHealth);
            currentColor.g *= (float)(health / _startingHealth);
            currentColor.b *= (float)(health / _startingHealth);
            GetComponent<SpriteRenderer>().color = currentColor;
        }

        private void SetStartingColor()
        {
            float colorIntensity = Sigmoid(health.Exponent, 2);
            _color = new Color(r: colorIntensity, g: -colorIntensity + 1, b: -colorIntensity + 1);
            GetComponent<SpriteRenderer>().color = _color;
            _startingColor = _color;
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
            powerUpController = powerUpController != null ? powerUpController : GameObject.Find("Manager").GetComponent<PowerUpController>();
            powerUpController.StartController();
        }
    }
}