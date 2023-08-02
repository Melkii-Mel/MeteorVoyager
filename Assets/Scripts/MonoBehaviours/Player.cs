using GameStatsNS;
using UnityEngine;
using UnityEngine.UI;
using static GameStatsNS.GameStats;
using Parameters = GameStatsNS.Parameters;

namespace MonoBehaviours
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private Slider chargingSlider;
        [SerializeField] private GameObject chargingFillArea;
        [SerializeField] private BulletEmitter emitter;
        private bool _controlDisabled;
        
        private float _cd;
        private float _charging;
        private readonly float _rBorder = Mathf.Deg2Rad * 75;
        private readonly float _nrBorder = Mathf.Deg2Rad * -75;
        
        #region events
        public delegate void PlayerEventsHandler();
        public static event PlayerEventsHandler OnShot;
        public static event PlayerEventsHandler OnChargedShot;
        
        #endregion
        
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
                    Shoot();
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
                OnShot?.Invoke();
                emitter.Shoot(spreadPower);
                _cd += GameStats.Parameters.ShotDelay;
                spreadPower += 10;
            }
            void Shoot()
            {
                int spreadPower = 0;
                if (_charging > 0.9f * MainGameStatsHolder.TurretUpgrades.ChargeAttack)
                {
                    PerformChargedShot();
                }
                else
                {
                    while (_cd < 0)
                    {
                        PerformShot(ref spreadPower);
                    }
                }
                _charging = 0;
                chargingFillArea.SetActive(false);
                chargingFillArea.GetComponent<Image>().color = ConvertStringToColor("#A7302D");
                chargingSlider.value = 0;
            }
            void FindTouchCoordinates(out float x, out float y)
            {
                Vector2 touch = Camera.main!.ScreenToWorldPoint(Input.mousePosition);
                var position = transform.position;
                x = touch.x - position.x;
                y = touch.y - position.y;
            }
            void ChargeChargedAttack()
            {
                _charging += Time.deltaTime * MainGameStatsHolder.TurretUpgrades.ChargeAttack;
                chargingFillArea.SetActive(true);
                chargingFillArea.GetComponent<Image>().color = ConvertStringToColor(_charging < 0.9f * MainGameStatsHolder.TurretUpgrades.ChargeAttack ? "#A7302D" : "#012A03");
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
                if (_cd <= -GameStats.Parameters.ShotDelay)
                {
                    return;
                }
                _cd -= Time.deltaTime;
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
                return DetectIfTouchPositionValid(y) && !IsSomeFieldEnabled && !_controlDisabled && _cd < 0;
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

        public void DisableControl()
        {
            _controlDisabled = true;
            var transform1 = transform;
            Quaternion quaternion = transform1.rotation;
            quaternion.x = 0;
            transform1.rotation = quaternion;
        }
        public void EnableControl()
        {
            _controlDisabled = false;
        }
    }
}