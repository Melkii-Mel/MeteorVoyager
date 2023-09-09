using System;
using System.Collections.Generic;
using GameStatsNS;
using UnityEngine;

namespace MonoBehaviours.UI
{
    /// <summary>
    /// Used to disable turret while popups are active
    /// </summary>
    public class PopUpActivityController : MonoBehaviour
    {
        [SerializeField] private List<GameObject> popUps;

        private void Update()
        {
            if (ForceFalse && ForceTrue) throw new Exception("Both of ForceTrue and ForceFalse values are true which is not allowed");
            if (ForceTrue)
            {
                GameStats.IsSomeFieldEnabled = true;
                return;
            }

            if (ForceFalse)
            {
                GameStats.IsSomeFieldEnabled = false;
                return;
            }
            
            foreach (GameObject popUp in popUps)
            {
                if (popUp.activeSelf)
                {
                    GameStats.IsSomeFieldEnabled = true;
                    return;
                }
            }
            GameStats.IsSomeFieldEnabled = false;
        }

        public static bool ForceTrue { get; set; }
        public static bool ForceFalse { get; set; }
    }
}