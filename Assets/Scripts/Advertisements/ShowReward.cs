using MeteorVoyager.Assets.Scripts.GameStatsNameSpace;
using UnityEngine;
using UnityEngine.UI;

namespace MeteorVoyager.Assets.Scripts.Advertisements
{
    public class ShowReward : MonoBehaviour
    {
        [SerializeField] Button showButton;
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