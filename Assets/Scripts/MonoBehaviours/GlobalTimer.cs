using UnityEngine;

namespace MonoBehaviours
{
    public class GlobalTimer : MonoBehaviour
    {
        [SerializeField] private float tickTimeSeconds;

        #region events;

        public static event Timer.OnTimerTickEventHandler OnTick;

        #endregion
        public static float TickTime;
        
        public void Start()
        {
            TickTime = tickTimeSeconds;
            new Timer(TickTime, OnTick, true);
        }
    }
}
