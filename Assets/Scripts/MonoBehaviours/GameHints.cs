using System;
using UnityEngine;
using UnityEngine.UI;
using static GameStatsNS.GameStats;

namespace MonoBehaviours
{
    public class GameHints : MonoBehaviour
    {
        [SerializeField] private GameObject hintScreen;
        [SerializeField] private Text hintText;
        public void ShowHint()
        {
            if (IsSomeFieldEnabled) return;

            hintScreen.SetActive(true);
            try
            {
                hintText.text = HintsTexts.Hints[MainGameStatsHolder.Progression.GameStage];
            }
            catch (IndexOutOfRangeException)
            {
                hintText.text = HintsTexts.UNIVERSAL_HINT;
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