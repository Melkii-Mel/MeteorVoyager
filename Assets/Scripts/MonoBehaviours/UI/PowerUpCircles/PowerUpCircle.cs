using UnityEngine;
using UnityEngine.UI;

namespace MonoBehaviours.UI.PowerUpCircles
{
    public abstract class PowerUpCircle : MonoBehaviour
    {
        [SerializeField] private Slider slider;
        private float _maxTime;
        private void Start()
        {
            _maxTime = GetTime();
            slider.value = 1;
        }

        private void Update()
        {
            float currentTime = GetTime();
            if (currentTime > _maxTime)
            {
                _maxTime = currentTime;
            }

            if (_maxTime == 0)
            {
                slider.value = 0;
                return;
            }
            slider.value = currentTime / _maxTime;
            if (currentTime == 0)
            {
                _maxTime = 0;
            }
        }

        protected abstract float GetTime();
    }
}