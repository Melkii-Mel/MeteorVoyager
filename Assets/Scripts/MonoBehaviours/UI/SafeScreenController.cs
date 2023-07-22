using GameStatsNS;
using UnityEngine;

namespace MonoBehaviours.UI
{
    [RequireComponent(typeof(SafeScreen))]
    public class SafeScreenController : MonoBehaviour
    {
        private bool _lastState;
        private SafeScreen _safeScreen;
        private void Awake()
        {
            _safeScreen = GetComponent<SafeScreen>();
            if (GameStats.MainGameStatsHolder.Settings.FitInSafeArea)
            {
                _lastState = true;
                _safeScreen.EnableSafeArea();
            }
            else
            {
                _lastState = false;
                _safeScreen.DisableSafeArea();
            }
        }

        private void Update()
        {
            if (_lastState == GameStats.MainGameStatsHolder.Settings.FitInSafeArea)
            {
                return;
            }

            if (_lastState)
            {
                _safeScreen.DisableSafeArea();
            }
            else
            {
                _safeScreen.EnableSafeArea();
            }
            _lastState = !_lastState;
        }
    }
}