using static MeteorVoyager.Assets.Scripts.GameStatsNameSpace.GameStats;
using Assets.Scripts.MonoBehaviours;
using MeteorVoyager.Assets.Scripts.GameStatsNameSpace;
using UnityEngine;
using UnityEngine.UI;
using System;

namespace MeteorVoyager.Assets.Scripts.MonoBehaviours
{
    public class Player : MonoBehaviour
    {
        public static Action OnShot { get; set; }
        public static Action OnChargedShot { get; set; }

        [SerializeField] Slider chargingSlider;
        [SerializeField] GameObject chargingFillArea;
        [SerializeField] BulletEmitter emitter;
        [Range(0f, 10f)] public float shotCooldown;
        private float cd;
        float charging = 0;
        readonly float RBorder = Mathf.Deg2Rad * 75;
        readonly float NRBorder = Mathf.Deg2Rad * -75;
        bool controlsDisabled;

        void Update()
        {
            if (Input.GetMouseButton(0) && !Input.GetMouseButton(1))
            {
                Vector2 touch = Input.mousePosition;
                touch = Camera.main.ScreenToWorldPoint(touch);
                float x = touch.x - transform.position.x;
                float y = touch.y - transform.position.y;
                if (y > 0 && !IsSomeFieldEnabled && !controlsDisabled)
                {
                    RotateTurretToTouch(x, y);
                    int spreadPower = 0;
                    if (charging > 0.9f * MainGameStatsHolder.TurretUpgrades.ChargeAttack)
                    {
                        OnChargedShot();
                        emitter.Shoot(charging: charging);
                        cd += Mathf.Sqrt(shotCooldown * 5);
                    }
                    else
                    {
                        while (cd + shotCooldown < 0)
                        {
                            OnShot();
                            emitter.Shoot(spreadPower);
                            cd += shotCooldown;
                            spreadPower += 10;
                        }
                        while (cd < -(shotCooldown * 20))
                        {
                            cd -= shotCooldown;
                        }
                    }
                    charging = 0;
                    chargingFillArea.SetActive(false);
                    chargingFillArea.GetComponent<Image>().color = SetColor("#A7302D");
                    chargingSlider.value = 0;
                    cd -= Time.deltaTime * (Mathf.Sqrt(MainGameStatsHolder.TurretUpgrades.ShotCooldown) + 1) / 2;
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
            charging += Time.deltaTime * MainGameStatsHolder.TurretUpgrades.ChargeAttack;
            chargingFillArea.SetActive(true);
            if (charging < 0.9f * MainGameStatsHolder.TurretUpgrades.ChargeAttack)
            {
                chargingFillArea.GetComponent<Image>().color = SetColor("#A7302D");
            }
            else
            {
                chargingFillArea.GetComponent<Image>().color = SetColor("#012A03");
            }
            if (charging > MainGameStatsHolder.TurretUpgrades.ChargeAttack) charging = MainGameStatsHolder.TurretUpgrades.ChargeAttack;
            if (MainGameStatsHolder.TurretUpgrades.ChargeAttack == 0)
            {
                chargingFillArea.SetActive(false);
            }
            else
            {
                chargingSlider.value = charging / MainGameStatsHolder.TurretUpgrades.ChargeAttack;
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