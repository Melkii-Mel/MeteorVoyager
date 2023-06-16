using Assets.Scripts.GameStatsNameSpace;
using Assets.Scripts.MonoBehaviours;
using MeteorVoyager.Assets.Scripts.GameStatsNameSpace;
using UnityEngine;
using UnityEngine.UI;

namespace MeteorVoyager.Assets.Scripts.MonoBehaviours
{
    public class Player : MonoBehaviour
    {
        [SerializeField] Slider chargingSlider;
        [SerializeField] GameObject chargingFillArea;
        [SerializeField] GameObject damageButton;
        [SerializeField] BulletEmitter emitter;
        public GameObject bullet;
        public GameObject enemy;
        public Vector3 worldPosition;
        public float cooldown;
        public float enemyCooldown;
        public float cd;
        public float eCd;
        float charging = 0;
        float RBorder;
        float NRBorder;

        bool controlsDisabled;
        bool enemiesCanSpawn;

        void Update()
        {
            if (Input.GetMouseButton(0) && !Input.GetMouseButton(1))
            {
                Vector2 touch = Input.mousePosition;
                touch = Camera.main.ScreenToWorldPoint(touch);
                float x = touch.x - transform.position.x;
                float y = touch.y - transform.position.y;
                if (y > 0 && !GameStats.IsSomeFieldEnabled && !controlsDisabled)
                {
                    RotateTurretToTouch(x, y);
                    int spreadPower = 0;
                    if (charging > 0.9f * TurretUpgrades.Instance.ChargeAttack)
                    {
                        emitter.Shoot(charging: charging);
                    }
                    else
                    {
                        while (cd + cooldown < 0)
                        {
                            emitter.Shoot(spreadPower);
                            cd += cooldown;
                            spreadPower += 10;
                        }
                        while (cd < -(cooldown * 20))
                        {
                            cd -= cooldown;
                        }
                    }
                    charging = 0;
                    chargingFillArea.SetActive(false);
                    chargingFillArea.GetComponent<Image>().color = SetColor("#A7302D");
                    chargingSlider.value = 0;
                    cd -= Time.deltaTime;
                }
                else
                {
                    Charge();
                }
            }
            else
            {
                Charge();
            }
        }

        public void DisableContol()
        {
            controlsDisabled = true;
            Quaternion quaternion = Quaternion.identity;
            quaternion.z = 0;
            transform.rotation = quaternion;
        }

        public void EnableContol()
        {
            controlsDisabled = false;
        }

        private void Charge()
        {
            charging += Time.deltaTime * TurretUpgrades.Instance.ChargeAttack;
            chargingFillArea.SetActive(true);
            if (charging < 0.9f * TurretUpgrades.Instance.ChargeAttack)
            {
                chargingFillArea.GetComponent<Image>().color = SetColor("#A7302D");
            }
            else
            {
                chargingFillArea.GetComponent<Image>().color = SetColor("#012A03");
            }
            if (charging > TurretUpgrades.Instance.ChargeAttack) charging = TurretUpgrades.Instance.ChargeAttack;
            if (TurretUpgrades.Instance.ChargeAttack == 0)
            {
                chargingFillArea.SetActive(false);
            }
            else
            {
                chargingSlider.value = charging / TurretUpgrades.Instance.ChargeAttack;
                chargingFillArea.SetActive(true);
            }
        }

        private Color SetColor(string input)
        {
            ColorUtility.TryParseHtmlString(input, out Color color);
            return color;
        }

        private void RotateTurretToTouch(float x, float y)
        {
            float angle = Mathf.Atan2(y, x) - Mathf.Deg2Rad * 90;
            Quaternion quat = transform.rotation;
            if (angle > RBorder)
            {
                angle = RBorder;
            }
            else if (angle < NRBorder)
            {
                angle = NRBorder;
            }
            quat.z -= (quat.z - angle / 2f) * 0.4f;
            transform.rotation = quat;
        }
    }
}