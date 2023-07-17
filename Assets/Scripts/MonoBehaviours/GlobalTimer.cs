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

        #region events;

        public static event Timer.OnTimerTickEventHandler OnTick;

        #endregion
        public static float TICK_TIME;
        
        public void Start()
        {
            TICK_TIME = tickTimeSeconds;
            new Timer(TICK_TIME, OnTick, true);
        }
    }
}
