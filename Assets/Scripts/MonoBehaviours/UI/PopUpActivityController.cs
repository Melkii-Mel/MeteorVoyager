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
    }
}