using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MonoBehaviours.DataBank
{
    public class Timer : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI timeText;
        [SerializeField] private Slider timerSlider;
        [SerializeField] private float time;
        private float _time;
        private bool _eventInvoked;

        public delegate void TimeEndEventHandler(Timer sender, DataBankTimerEventArgs args);

        public event TimeEndEventHandler OnTimeEnd;

        public void Awake()
        {
            enabled = false;
        }

        public void StartTimer()
        {
            _time = time;
            enabled = true;
        }

        private void Update()
        {
            if (_time < 0)
            {
                if (_eventInvoked) return;
                OnTimeEnd?.Invoke(this, new DataBankTimerEventArgs(time: time));
                _eventInvoked = true;
            }

            _time -= Time.deltaTime;
            timerSlider.value = _time / time;
        }

        public class DataBankTimerEventArgs
        {
            public float Time;

            public DataBankTimerEventArgs(float time)
            {
                Time = time;
            }
        }
    }
}