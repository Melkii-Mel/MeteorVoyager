using System;
using TMPro;
using UnityEngine;
using static GameStatsNS.GameStats;

namespace MonoBehaviours
{
    public class GameHints : MonoBehaviour
    {
        [SerializeField] private GameObject hintScreen;
        [SerializeField] private TextMeshProUGUI hintText;

        public void Update()
        {
            if (hintScreen.activeSelf)
            {
                ShowHint();
            }
        }

        private void ShowHint()
        {
            if (IsSomeFieldEnabled) return;

            hintScreen.SetActive(true);
            try
            {
                hintText.text = Texts.StageTexts.StageTexts[MainGameStatsHolder.Progression.GameStage];
            }
            catch (IndexOutOfRangeException)
            {
                hintText.text = Texts.StageTexts.UniversalStage;
            }
            IsSomeFieldEnabled = true;
        }
        public void Close()
        {
            hintScreen.SetActive(false);
            IsSomeFieldEnabled = false;
        }
    }
}