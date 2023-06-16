using Assets.Scripts.GameStatsNameSpace;
using UnityEngine;

namespace MeteorVoyager.Assets.Scripts.MonoBehaviours
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] GameObject explosion;
        Transform player;
        public int ricochetCounter;
        public int pierceCounter;
        public float chargedPierceCoefficient = 1;
        public float chargedDamageCoefficient = 1;
        public bool isCharged;
        float _rBorder;
        float _lBorder;
        [SerializeField] float timer;
        float _scaleCounter;
        Vector3 _scale;
        void Start()
        {
            _scale = transform.localScale;
            GetComponent<TrailRenderer>().enabled = SettingsGameStats.Instance.TrailsEnabled;
            player = GameObject.Find("Player").GetComponent<Transform>();
            UpdateStats();
            _rBorder = Consts.RBorder;
            _lBorder = Consts.LBorder;
            if (timer == 0) timer = 0.5f;

        }

        void Update()
        {
            if (isCharged)
            {
                if (_scaleCounter < 2)
                {
                    GetComponent<TrailRenderer>().widthMultiplier *= 1.01f;
                    transform.localScale = _scale * (_scaleCounter + 1);
                    _scaleCounter += Time.deltaTime;
                }
            }
            transform.Translate(new Vector2(0, 3 * (!isCharged ? Mathf.Log(TurretUpgrades.Instance.Damage + 1, 4) : 1) * Time.deltaTime));
            timer -= Time.deltaTime;
            if (timer > 0) gameObject.transform.rotation = player.rotation;
            float x = transform.position.x;
            if (x < _lBorder || x > _rBorder)
            {
                Destroy(gameObject);
            }
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            collision.gameObject.GetComponent<Enemy>().DealDamage(DamageCalculator.CalculateDefaultDamage());
            if (Timers.Instance.ExplosivesAttacksTimer > 0)
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
            pierceCounter = (int)(TurretUpgrades.Instance.PierceCount * chargedPierceCoefficient);
        }
    }
}