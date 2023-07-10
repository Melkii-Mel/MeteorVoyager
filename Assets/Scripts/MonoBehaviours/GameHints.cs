using MeteorVoyager.Assets.Scripts.GameStatsNameSpace;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace MeteorVoyager.Assets.Scripts.MonoBehaviours
{
    public class GameHints : MonoBehaviour
    {
        [SerializeField] GameObject hintScreen;
        [SerializeField] Text hintText;
        public void ShowHint()
        {
            if (GameStats.IsSomeFieldEnabled) return;

            hintScreen.SetActive(true);
            try
            {
                hintText.text = HintsTexts.HINTS[GameStats.MainGameStatsHolder.Progression.GameStage];
            }
            catch (IndexOutOfRangeException)
            {
                hintText.text = HintsTexts.UNIVERSAL_HINT;
            }
            GameStats.IsSomeFieldEnabled = true;
        }
        public void Close()
        {
            hintScreen.SetActive(false);
            GameStats.IsSomeFieldEnabled = false;
        }
    }
}