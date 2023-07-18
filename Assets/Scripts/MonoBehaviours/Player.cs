using System;
using UnityEngine;
using UnityEngine.UI;
using static GameStatsNS.GameStats;

namespace MonoBehaviours
{
    public class Player : MonoBehaviour
    {
        public static Action OnShot { get; set; }
        public static Action OnChargedShot { get; set; }

        [SerializeField] private Slider chargingSlider;
        [SerializeField] private GameObject chargingFillArea;
        [SerializeField] private BulletEmitter emitter;
        [Range(0f, 10f)] public float shotCooldown;
        private bool _controlDisabled;


        private float _cd;
        private float _charging = 0;
        private readonly float _rBorder = Mathf.Deg2Rad * 75;
        private readonly float _nrBorder = Mathf.Deg2Rad * -75;

        private void Update()
        {
            if (!Touched())
            {
                ChargeChargedAttack();
                DecreaseCooldown();
            }
            else
            {
                FindTouchCoordinates(out float x, out float y);
                if (DetectIfTouchPositionValid(y) && !_controlDisabled)
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
                emitter.Shoot(charging: _charging);
                _cd = 0.3f;
            }
            void PerformShot(ref int spreadPower)
            {
                OnShot();
                emitter.Shoot(spreadPower);
                _cd += shotCooldown;
                spreadPower += 10;
            }
            void LimitCooldownStacking()
            {
                while (_cd < -(shotCooldown * 20))
                {
                    _cd -= shotCooldown;
                }
            }
            void Shoot(float x, float y)
            {
                int spreadPower = 0;
                if (_charging > 0.9f * MainGameStatsHolder.TurretUpgrades.ChargeAttack)
                {
                    PerformChargedShot();
                }
                else
                {
                    while (_cd + shotCooldown < 0)
                    {
                        PerformShot(ref spreadPower);
                    }
                    LimitCooldownStacking();
                }
                _charging = 0;
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
                _charging += Time.deltaTime * MainGameStatsHolder.TurretUpgrades.ChargeAttack;
                chargingFillArea.SetActive(true);
                if (_charging < 0.9f * MainGameStatsHolder.TurretUpgrades.ChargeAttack)
                {
                    chargingFillArea.GetComponent<Image>().color = ConvertStringToColor("#A7302D");
                }
                else
                {
                    chargingFillArea.GetComponent<Image>().color = ConvertStringToColor("#012A03");
                }
                if (_charging > MainGameStatsHolder.TurretUpgrades.ChargeAttack) _charging = MainGameStatsHolder.TurretUpgrades.ChargeAttack;
                if (MainGameStatsHolder.TurretUpgrades.ChargeAttack == 0)
                {
                    chargingFillArea.SetActive(false);
                }
                else
                {
                    chargingSlider.value = _charging / MainGameStatsHolder.TurretUpgrades.ChargeAttack;
                    chargingFillArea.SetActive(true);
                }
            }
            void DecreaseCooldown()
            {
                if (_cd <= -shotCooldown)
                {
                    return;
                }
                _cd -= Time.deltaTime * (Mathf.Sqrt(MainGameStatsHolder.TurretUpgrades.ShotCooldown) + 1);
            }
            void RotateTurretToTouch(float x, float y)
            {
                float angle = Mathf.Atan2(y, x) - Mathf.Deg2Rad * 90;
                Quaternion quat = transform.rotation;
                if (angle > _rBorder)
                {
                    angle = _rBorder;
                }
                else if (angle < _nrBorder)
                {
                    angle = _nrBorder;
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
                return DetectIfTouchPositionValid(y) && !IsSomeFieldEnabled && !_controlDisabled && _cd + shotCooldown < 0;

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
            _controlDisabled = true;
            Quaternion quaternion = transform.rotation;
            quaternion.x = 0;
            transform.rotation = quaternion;
        }
        public void EnableContol()
        {
            _controlDisabled = false;
        }
    }
}