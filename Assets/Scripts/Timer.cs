using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Timer
{
    public float IntervalMS { get; set; }

    public delegate void TimerTickEventHandler(float deltaTimeMS);

    public event TimerTickEventHandler OnTimerTick;

    public bool Running { get; private set; }

    public Timer(float intervalMS, TimerTickEventHandler @event, bool enableOnStart, bool stopOnSceneChange = true)
    {
        if (intervalMS <= 0)
        {
            throw InvalidIntervalException;
        }
        IntervalMS = intervalMS;
        OnTimerTick = @event;
        if (enableOnStart)
        {
            StartTimer();
        }

        if (stopOnSceneChange)
        {
            SceneManager.sceneUnloaded += (_) => Stop();
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

    private async void StartCoroutine()
    {
        if (Running)
        {
            return;
        }
        Running = true;
        await Tick();
    }

    private void StopCoroutine()
    {
        Running = false;
    }

    private async Task Tick()
    {
        while (Running)
        {
            if (!Application.isPlaying)
            {
                StopCoroutine();
                return;
            }
            OnTimerTick?.Invoke(IntervalMS);
            await Task.Delay((int)(IntervalMS));
        }
    }

    private static readonly Exception InvalidIntervalException = new("Invalid timer interval. Must be greater than 0");
}