using UnityEngine;

namespace MonoBehaviours
{
    [DisallowMultipleComponent]
    public class GlobalTimer : MonoBehaviour
    {
        [SerializeField] private float tickTimeSeconds;
        private Timer _timer;

        #region events;

        public static event Timer.TimerTickEventHandler OnTick;

        public static float TickTime { get; private set; }
        
        #endregion
        
        public void Start()
        {
            TickTime = tickTimeSeconds * 1000;
            _timer = new Timer(TickTime, OnTick, true);
        }

        public void OnDestroy()
        {
            _timer.Stop();
            _timer = null;
        }
    }
}
