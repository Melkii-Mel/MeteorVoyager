using System;
using System.Threading.Tasks;
using UnityEngine;

namespace MeteorVoyager.Assets.Scripts.MonoBehaviours
{
    public class Timer
    {
        public float IntervalS { get; set; } = 1;
        public Action OnTick { get; set; }
        public bool Running { get; private set; } = false;

        public Timer(float intervalS, Action action, bool enableOnStart)
        {
            if (intervalS <= 0)
            {
                throw InvalidIntervalException;
            }
            IntervalS = intervalS;
            OnTick = action;
            if (enableOnStart)
            {
                StartTimer();
            }
        }
        public void StartTimer()
        {
            SetCoroutineActivity(true);
        }
        public void Stop()
        {
            SetCoroutineActivity(false);
        }
        private void SetCoroutineActivity(bool activity)
        {
            if (activity)
            {
                StartCoroutine();
            }
            else
            {
                StopCoroutine();
            }
        }
        async void StartCoroutine()
        {
            if (Running == true)
            {
                return;
            }
            Running = true;
            await Tick();
        }
        void StopCoroutine()
        {
            Running = false;
        }
        async Task Tick()
        {
            while (Running)
            {
                if (!Application.isPlaying)
                {
                    StopCoroutine();
                    return;
                }
                OnTick();
                await Task.Delay((int)(IntervalS * 1000));
            }
        }

        private static readonly Exception InvalidIntervalException = new("Invalid timer interval. Must be greater than 0");
    }
}