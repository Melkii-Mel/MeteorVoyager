using UnityEngine;
using UnityEngine.UI;

namespace MonoBehaviours.UI.PowerUpCircles
{
    public abstract class PowerUpCircle : MonoBehaviour
    {
        [SerializeField] private Slider slider;
        private void Update()
        {
            if (GetTime() > 0)
            {
                slider.value = 1;
            }
            else
            {
                slider.value -= 0.1f;
            }
            
        }

        protected abstract float GetTime();
    }
}