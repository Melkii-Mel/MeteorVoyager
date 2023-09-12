using System;
using UnityEngine;
using UnityEngine.UI;
using static GameStatsNS.GameStats;

namespace MonoBehaviours.UI
{
    public class BalanceTexts : MonoBehaviour
    {
        [SerializeField] private GameObject textBalance;
        [SerializeField] private GameObject textData;

        private void OnEnable()
        {
            MainGameStatsHolder.Progression.OnProgressionUpdate += DataUnlockCheck;
            DataUnlockCheck();
        }

        private void OnDisable()
        {
            MainGameStatsHolder.Progression.OnProgressionUpdate -= DataUnlockCheck;
        }

        private void Update()
        {
            textBalance.GetComponent<Text>().text = $"{Texts.CurrencyTexts.Matter}: " + MainGameStatsHolder.Currency.Balance.ToString();
            textData.GetComponent<Text>().text = $"{Texts.CurrencyTexts.Data}: " + MainGameStatsHolder.Currency.Data.ToString();
        }

        private void DataUnlockCheck()
        {
            if (MainGameStatsHolder.Progression.GameStage >= 4)
            {
                textData.SetActive(true);
                MainGameStatsHolder.Progression.OnProgressionUpdate -= DataUnlockCheck;
            }
            else
            {
                textData.SetActive(false);
            }
        }
    }
}