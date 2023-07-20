using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Timer
{
    public float IntervalS { get; set; }

    public delegate void OnTimerTickEventHandler(float deltaTimeMS);

    public event OnTimerTickEventHandler OnTimerTick;

    public bool Running { get; private set; }

    public Timer(float intervalS, OnTimerTickEventHandler @event, bool enableOnStart, bool stopOnSceneChange = true)
    {
        if (intervalS <= 0)
        {
            throw InvalidIntervalException;
        }
        IntervalS = intervalS;
        OnTimerTick = @event;
        if (enableOnStart)
        {
            StartTimer();
        }

        if (stopOnSceneChange)
        {
            SceneManager.sceneUnloaded += (scene) => Stop();
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
            OnTimerTick?.Invoke(IntervalS * 1000);
            await Task.Delay((int)(IntervalS * 1000));
        }
    }

    private static readonly Exception InvalidIntervalException = new("Invalid timer interval. Must be greater than 0");
}