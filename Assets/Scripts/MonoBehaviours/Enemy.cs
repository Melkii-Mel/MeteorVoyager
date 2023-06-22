using static MeteorVoyager.Assets.Scripts.GameStatsNameSpace.GameStats;
using Unity.Mathematics;
using UnityEngine;
using MeteorVoyager.Assets.Scripts.GameStatsNameSpace;

namespace MeteorVoyager.Assets.Scripts.MonoBehaviours
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private GameObject particles;
        [SerializeField] private PowerUpController powerUpController;
        float lifetime = 20;
        Color color;
        public InfiniteInteger health = 1;
        public static InfiniteInteger maxEnemyHealth = 1;
        InfiniteInteger startingHealth;
        public float speed;
        public bool isGlowing;
        private void Start()
        {
            GetComponent<TrailRenderer>().enabled = MainGameStatsHolder.Settings.TrailsEnabled;
            transform.GetChild(0).gameObject.SetActive(isGlowing);
            startingHealth = health;
            SetStartingColor();
        }

        void Update()
        {
            transform.Translate(new Vector2(0, -3) * (Time.deltaTime * speed * (isGlowing ? 0.5f : 1)));
            lifetime -= Time.deltaTime;
            if (lifetime < 0)
            {
                EnemySpawner.Enemies.Remove(gameObject);
                Destroy(gameObject);
            }
        }

        public void Undo()
        {
            if (isGlowing)
            {
                GivePowerUp();
            }
            if (MainGameStatsHolder.Settings.ParticlesEnabled)
            {
                try
                {
                    SpriteRenderer particle = Instantiate(particles, transform.position, transform.rotation).GetComponent<SpriteRenderer>();
                    particle.color = color;
                }
                catch { }
            }
            EnemySpawner.Enemies.Remove(gameObject);
            Destroy(gameObject);
        }
        public void DealDamage(InfiniteInteger damage)
        {
            ChangeColor();
            if (MainGameStatsHolder.Settings.ParticlesEnabled)
            {
                Instantiate(particles, transform.position, transform.rotation);
            }
            InfiniteInteger prevHealth = health;
            if (damage < 1)
            {
                damage = 1;
            }
            health -= damage;
            if (health > 0)
            {
                GiveMatter(damage);
            }
            else
            {
                GiveMatter(prevHealth);
            }
            if (health <= 0) Undo();
        }

        private static void GiveMatter(InfiniteInteger damage)
        {
            InfiniteInteger reward = damage;
            reward *= MainGameStatsHolder.Timers.CoinMultiplierTimer > 0 ? 3 : 1;
            reward *= MainGameStatsHolder.Timers.X10Reward > 0 ? 10 : 1;
            reward *= (int)Mathf.Pow(2, MainGameStatsHolder.MeteorUpgrades.CoinMultiplier);
            MainGameStatsHolder.Currency.Balance += reward;
        }

        float Sigmoida(float value, float displacement = 0)
        {
            value = 1f / (1f + Mathf.Pow(math.E, Mathf.Sqrt(value) - displacement));
            return Mathf.Pow(value, 0.25f);
        }

        private void ChangeColor()
        {
            Color currentColor = color;
            currentColor.r *= (float)(health / startingHealth);
            currentColor.g *= 1 - (float)(health / startingHealth);
            currentColor.b *= 1 - (float)(health / startingHealth);
            GetComponent<SpriteRenderer>().color = currentColor;
            GetComponent<TrailRenderer>().startColor = currentColor;
        }

        private void SetStartingColor()
        {
            float colorIntencity = Sigmoida(health.Exponent, 2);
            GetComponent<SpriteRenderer>().color = new Color(r: colorIntencity, g: -colorIntencity + 1, b: -colorIntencity + 1);
            color = GetComponent<SpriteRenderer>().color;
            GetComponent<TrailRenderer>().startColor = color;
        }

        private void GivePowerUp()
        {
            const float COEFF = 0.1f;
            int randint = UnityEngine.Random.Range(0, 3);
            switch (randint)
            {
                case 0: MainGameStatsHolder.Timers.AddTime(MainGameStatsHolder.MeteorUpgrades.CoinMultiplierTimeUpgrade * COEFF, Timers.Timer.CoinMultiplierTimer); break;
                case 1: MainGameStatsHolder.Timers.AddTime(MainGameStatsHolder.MeteorUpgrades.DamageMultiplierTimeUpgrade * COEFF, Timers.Timer.DamageMultiplierTimer); break;
                case 2: MainGameStatsHolder.Timers.AddTime(MainGameStatsHolder.MeteorUpgrades.ExplosivesAttacksTimeUpgrade * COEFF, Timers.Timer.ExplosivesAttacksTimer); break;
            }
            powerUpController = powerUpController != null ? powerUpController : GameObject.Find("Manager").GetComponent<PowerUpController>();
            powerUpController.StartController();
        }
    }
}