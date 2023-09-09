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
            MainGameStatsHolder.Progression.OnProgressionUpdate += DataUnlockChecker;
        }

        private void OnDisable()
        {
            MainGameStatsHolder.Progression.OnProgressionUpdate -= DataUnlockChecker;
        }

        private void Update()
        {
            textBalance.GetComponent<Text>().text = $"{Texts.CurrencyTexts.Matter}: " + MainGameStatsHolder.Currency.Balance.ToString();
            textData.GetComponent<Text>().text = $"{Texts.CurrencyTexts.Data}: " + MainGameStatsHolder.Currency.Data.ToString();
        }

        private void DataUnlockChecker()
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