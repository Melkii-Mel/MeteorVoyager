using GameStatsNS;
using MonoBehaviours.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Settings
{
    public class SafeScreenToggle : MonoBehaviour
    {
        // Start is called before the first frame update
        [SerializeField] private SafeScreen safeScreen;
        void Awake()
        {
            GetComponent<Toggle>().isOn = GameStats.MainGameStatsHolder.Settings.FitInSafeArea;
        }

        // Update is called once per frame
        public void OnUpdate(bool value)
        {
            GameStats.MainGameStatsHolder.Settings.FitInSafeArea = value;
            if (value)
            {
                safeScreen.EnableSafeArea();
                return;
            }
            safeScreen.DisableSafeArea();
        }
    }
}
