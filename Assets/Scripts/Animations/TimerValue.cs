using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Animations
{
    public class TimerValue : MonoBehaviour
    {
        [SerializeField] private float min;

        [SerializeField] private float max;

        [SerializeField] private bool whole;
        [SerializeField] private Slider timerSlider;
        [SerializeField] private TextMeshProUGUI text;
        void Update()
        {
            float value = min + (max - min) * timerSlider.normalizedValue;
            if (whole)
            {
                value = (int)value;
            }

            text.text = value.ToString(CultureInfo.CurrentCulture);
        }
    }
}
