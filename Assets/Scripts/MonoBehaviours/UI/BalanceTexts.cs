using static MeteorVoyager.Assets.Scripts.GameStatsNameSpace.GameStats;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace MeteorVoyager.Assets.Scripts.MonoBehaviours
{
    public class BalanceTexts : MonoBehaviour
    {
        [SerializeField] GameObject textBalance;
        [SerializeField] GameObject textData;
        void Start()
        {
            MainGameStatsHolder.Progression.OnProgressionUpdate += DataUnlockChecker;
        }
        void Update()
        {
            textBalance.GetComponent<Text>().text = $"{Texts.CurrencyTexts.Matter}: " + MainGameStatsHolder.Currency.Balance.ToString();
            textData.GetComponent<Text>().text = $"{Texts.CurrencyTexts.Data}: " + MainGameStatsHolder.Currency.Data.ToString();
        }
        void DataUnlockChecker()
        {
            if (MainGameStatsHolder.Progression.GameStage >= 4)
            {
                textData.SetActive(true);
                MainGameStatsHolder.Progression.OnProgressionUpdate -= DataUnlockChecker;
            }
            else
            {
                textData.SetActive(false);
            }
        }
    }
}