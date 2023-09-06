using MonoBehaviours.Interfaces;
using UnityEngine;
using static GameStatsNS.GameStats;

namespace MonoBehaviours
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private GameObject explosion;
        public Transform EmitterTransform { get; set; }
        public int PierceCounter { get; set; }
        public InfiniteInteger Damage { get; set; }
        public bool IsCharged { get; set; }
        private float _rotationTimer = 0.5f;
        private float _lifeTimer = 5;
        private float _scaleMultiplier;
        [SerializeField] private float speedMultiplier = 5;
        private float _chargedBulletSpeedMultiplier = 1;
        private Vector3 _scale;

        private void Start()
        {
            _scale = transform.localScale;
            GetComponent<TrailRenderer>().enabled = MainGameStatsHolder.Settings.TrailsEnabled;
        }

        private void Update()
        {
            if (IsCharged)
            {
                if (_scaleMultiplier < 2)
                {
                    transform.localScale = _scale * (_scaleMultiplier + 1);
                    _scaleMultiplier += Time.deltaTime;
                }
            }
            transform.Translate(new Vector2(0,
                3 * (!IsCharged ? Mathf.Log(MainGameStatsHolder.TurretUpgrades.Damage + 2, 4) / 5 + 0.5f : 1) *
                Time.deltaTime * speedMultiplier * _chargedBulletSpeedMultiplier));
            if (_rotationTimer > 0)
            {
                _rotationTimer -= Time.deltaTime;
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
            if (!collision.CompareTag("IDamageable")) return;
            collision.gameObject.TryGetComponent(out IDamageable damageable);
            
            damageable.TakeDamage(Damage, transform.rotation);
            if (IsCharged)
            {
                _chargedBulletSpeedMultiplier *= 0.99f;
                if (_chargedBulletSpeedMultiplier <= 0.05f)
                {
                    Destroy(gameObject);
                }
            }
            if (MainGameStatsHolder.Timers.ExplosivesAttacksTimer > 0)
            {
                var transform1 = transform;
                Instantiate(explosion, transform1.position, transform1.rotation);
            }
            PierceCounter--;
            if (PierceCounter < 0)
            {
                Destroy(gameObject);
            }
        }
    }
}