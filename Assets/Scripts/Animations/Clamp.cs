using System;
using MonoBehaviours;
using UnityEngine;
using UnityEngine.Serialization;

namespace Animations
{
    public class Clamp : MonoBehaviour
    {
        [SerializeField] private Transform bone0L;
        [SerializeField] private Transform bone0R;
        [SerializeField] private Transform bone1L;
        [SerializeField] private Transform bone1R;
        private Quaternion _bone0LRotationStart;
        private Quaternion _bone0RRotationStart;
        private Quaternion _bone1LRotationStart;
        private Quaternion _bone1RRotationStart;


        [SerializeField] private float bone0Coeff;
        [SerializeField] private float bone1Coeff;

        private float _dynamicCoeff0 = 1;
        private float _dynamicCoeff1 = 1;

        public void OnEnable()
        {
            GlobalTimer.OnTick += TickUnclamp;
            Player.OnShot += ClampDefault;
            Player.OnChargedShot += ClampCharged;
        }

        public void OnDisable()
        {
            GlobalTimer.OnTick -= TickUnclamp;
            Player.OnShot -= ClampDefault;
            Player.OnChargedShot -= ClampCharged;
        }

        public void Start()
        {
            _bone0LRotationStart = bone0L.localRotation;
            _bone0RRotationStart = bone0R.localRotation;
            _bone1LRotationStart = bone1L.localRotation;
            _bone1RRotationStart = bone1R.localRotation;
            
        }

        public void Update()
        {
            bone0L.localRotation = _bone0LRotationStart * Quaternion.Euler(0, 0, _dynamicCoeff0);
            bone0R.localRotation = _bone0RRotationStart * Quaternion.Euler(0, 0, -_dynamicCoeff0);
            bone1L.localRotation = _bone1LRotationStart * Quaternion.Euler(0, 0, _dynamicCoeff1);
            bone1R.localRotation = _bone1RRotationStart * Quaternion.Euler(0, 0, -_dynamicCoeff1);
            
        }

        private void ClampDefault()
        {
            _dynamicCoeff0 = bone0Coeff;
            _dynamicCoeff1 = bone1Coeff;
        }

        private void ClampCharged()
        {
            _dynamicCoeff0 = -bone0Coeff * 2;
            _dynamicCoeff1 = -bone1Coeff * 2;
        }

        private const float STEP_MULTIPLIER = 0.9f;

        private void TickUnclamp(float _)
        {
            _dynamicCoeff0 *= STEP_MULTIPLIER;
            _dynamicCoeff1 *= STEP_MULTIPLIER;
        }
    }
}
