using Assets.Scripts.MonoBehaviours;
using System;
using UnityEngine;
using UnityEngine.UI;
using static MeteorVoyager.Assets.Scripts.GameStatsNameSpace.GameStats;

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
        bool controlDisabled;


        private float cd;
        private float charging = 0;
        private readonly float RBorder = Mathf.Deg2Rad * 75;
        private readonly float NRBorder = Mathf.Deg2Rad * -75;
        void Update()
        {
            if (!Touched())
            {
                ChargeChargedAttack();
                DecreaseCooldown();
            }
            else
            {
                FindTouchCoordinates(out float x, out float y);
                if (DetectIfTouchPositionValid(y) && !controlDisabled)
                {
                    RotateTurretToTouch(x, y);
                }

                if (DetectIfCanShoot(y))
                {
                    Shoot(x, y);
                }
                else
                {
                    ChargeChargedAttack();
                    DecreaseCooldown();
                }
            }
            #region local functions
            void PerformChargedShot()
            {
                OnChargedShot();
                emitter.Shoot(charging: charging);
                cd = 0.3f;
            }
            void PerformShot(ref int spreadPower)
            {
                OnShot();
                emitter.Shoot(spreadPower);
                cd += shotCooldown;
                spreadPower += 10;
            }
            void LimitCooldownStacking()
            {
                while (cd < -(shotCooldown * 20))
                {
                    cd -= shotCooldown;
                }
            }
            void Shoot(float x, float y)
            {
                int spreadPower = 0;
                if (charging > 0.9f * MainGameStatsHolder.TurretUpgrades.ChargeAttack)
                {
                    PerformChargedShot();
                }
                else
                {
                    while (cd + shotCooldown < 0)
                    {
                        PerformShot(ref spreadPower);
                    }
                    LimitCooldownStacking();
                }
                charging = 0;
                chargingFillArea.SetActive(false);
                chargingFillArea.GetComponent<Image>().color = ConvertStringToColor("#A7302D");
                chargingSlider.value = 0;
            }
            void FindTouchCoordinates(out float x, out float y)
            {
                Vector2 touch = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                x = touch.x - transform.position.x;
                y = touch.y - transform.position.y;
            }
            void ChargeChargedAttack()
            {
                charging += Time.deltaTime * MainGameStatsHolder.TurretUpgrades.ChargeAttack;
                chargingFillArea.SetActive(true);
                if (charging < 0.9f * MainGameStatsHolder.TurretUpgrades.ChargeAttack)
                {
                    chargingFillArea.GetComponent<Image>().color = ConvertStringToColor("#A7302D");
                }
                else
                {
                    chargingFillArea.GetComponent<Image>().color = ConvertStringToColor("#012A03");
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
            void DecreaseCooldown()
            {
                if (cd <= -shotCooldown)
                {
                    return;
                }
                cd -= Time.deltaTime * (Mathf.Sqrt(MainGameStatsHolder.TurretUpgrades.ShotCooldown) + 1) / 2;
            }
            void RotateTurretToTouch(float x, float y)
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
                quat.x -= (quat.x + angle / 2f) * 0.4f;
                transform.rotation = quat;
            }
            bool Touched()
            {
                return Input.GetMouseButton(0) && !Input.GetMouseButton(1);
            }
            bool DetectIfCanShoot(float y)
            {
                return DetectIfTouchPositionValid(y) && !IsSomeFieldEnabled && !controlDisabled && cd + shotCooldown < 0;

            }
            bool DetectIfTouchPositionValid(float y)
            {
                return y > 0;
            }
            Color ConvertStringToColor(string input)
            {
                ColorUtility.TryParseHtmlString(input, out Color color);
                return color;
            }
            #endregion
        }

        public void DisableContol()
        {
            controlDisabled = true;
            Quaternion quaternion = transform.rotation;
            quaternion.x = 0;
            transform.rotation = quaternion;
        }
        public void EnableContol()
        {
            controlDisabled = false;
        }
    }
}