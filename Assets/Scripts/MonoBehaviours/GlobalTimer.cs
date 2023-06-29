using MeteorVoyager.Assets.Scripts.MonoBehaviours;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MeteorVoyager
{
    public class GlobalTimer : MonoBehaviour
    {
        [SerializeField] float tickTimeSeconds;

        static Action OnTimerTick { get; set; } = delegate { };
        public static float TICK_TIME;
        
        public void Start()
        {
            TICK_TIME = tickTimeSeconds;
            new Timer(TICK_TIME, OnTimerTick, true);
        }

        public static void AddAction(Action action)
        {
            OnTimerTick += action;
        }
        public static void RemoveAction(Action action)
        {
            OnTimerTick -= action;
        }
    }
}
