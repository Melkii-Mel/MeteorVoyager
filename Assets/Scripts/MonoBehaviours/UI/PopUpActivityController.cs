using MeteorVoyager.Assets.Scripts.GameStatsNameSpace;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MeteorVoyager.Assets.Scripts.MonoBehaviours
{
    /// <summary>
    /// Used to disable turret while popups are active
    /// </summary>
    public class PopUpActivityController : MonoBehaviour
    {
        [SerializeField] List<GameObject> popUps;
        void Update()
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