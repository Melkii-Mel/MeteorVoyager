using MonoBehaviours.Interfaces;
using UnityEngine;
using UnityEngine.Serialization;
using static GameStatsNS.GameStats;

namespace MonoBehaviours
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private GameObject explosion;
        public Transform EmitterTransform { get; set; }
        public int ricochetCounter;
        public int pierceCounter;
        public float chargedPierceCoefficient = 1;
        public float chargedDamageCoefficient = 1;
        public bool isCharged;
        [SerializeField][Range(0.5f, 10f)] private float timer;
        private float _lifeTimer = 5;
        private float _scaleCounter;
        [FormerlySerializedAs("SPEED_MULTIPLIER")] [SerializeField] private float speedMultiplier = 5;
        private float _chargedBulletSpeedMultiplier = 1;
        private Vector3 _scale;

        private void Start()
        {
            _scale = transform.localScale;
            GetComponent<TrailRenderer>().enabled = MainGameStatsHolder.Settings.TrailsEnabled;
            UpdateStats();
        }

        private void Update()
        {
            if (isCharged)
            {
                if (_scaleCounter < 2)
                {
                    transform.localScale = _scale * (_scaleCounter + 1);
                    _scaleCounter += Time.deltaTime;
                }
            }
            transform.Translate(new Vector2(0, 3 * (!isCharged ? Mathf.Log(MainGameStatsHolder.TurretUpgrades.Damage + 2, 4) / 5 + 0.5f: 1) * Time.deltaTime * speedMultiplier * _chargedBulletSpeedMultiplier));
            if (timer > 0)
            {
                timer -= Time.deltaTime;
                gameObject.transform.rotation = EmitterTransform.rotation;
            }
            _lifeTimer -= Time.deltaTime;
            if (_lifeTimer < 0)
            {
                Destroy(gameObject);
            }
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            collision.gameObject.GetComponent<IDamageable>().TakeDamage(DamageCalculator.CalculateDefaultDamage());
            if (isCharged)
            {
                _chargedBulletSpeedMultiplier *= 0.99f;
                if (_chargedBulletSpeedMultiplier <= 0.1f)
                {
                    Destroy(gameObject);
                }
            }
            if (MainGameStatsHolder.Timers.ExplosivesAttacksTimer > 0)
            {
                Instantiate(explosion, gameObject.transform.position, gameObject.transform.rotation);
            }
            CheckPierces();
        }

        private void CheckPierces()
        {
            pierceCounter--;
            if (pierceCounter < 0)
            {
                Destroy(gameObject);
            }
        }
        public void UpdateStats()
        {
            if (chargedDamageCoefficient < 1) chargedDamageCoefficient = 1;
            if (chargedPierceCoefficient < 1) chargedPierceCoefficient = 1;
            pierceCounter = (int)(MainGameStatsHolder.TurretUpgrades.PierceCount * chargedPierceCoefficient);
        }
    }
}