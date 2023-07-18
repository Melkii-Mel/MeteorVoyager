using GameStatsNS;
using UnityEngine;
using UnityEngine.UI;

namespace Advertisements
{
    public class ShowReward : MonoBehaviour
    {
        [SerializeField] private Button showButton;
        public void Open()
        {
            if (GameStats.IsSomeFieldEnabled) return;

            gameObject.SetActive(true);
            showButton.interactable = false;
            GameStats.IsSomeFieldEnabled = true;
        }
        public void Close()
        {
            gameObject.SetActive(false);
            showButton.interactable = true;
            GameStats.IsSomeFieldEnabled = false;
        }
    }
}