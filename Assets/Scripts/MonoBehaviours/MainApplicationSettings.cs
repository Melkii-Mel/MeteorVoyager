using GameStatsNS;
using UnityEngine;

namespace MonoBehaviours
{
    public class MainApplicationSettings : MonoBehaviour
    {
        void Awake()
        {
            Application.targetFrameRate = GameStats.MainGameStatsHolder.Settings.FrameRate;
        }
    }
}