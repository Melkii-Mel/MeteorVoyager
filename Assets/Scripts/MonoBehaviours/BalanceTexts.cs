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
            StartCoroutine(DataUnlockChecker());
        }
        void Update()
        {
            textBalance.GetComponent<Text>().text = "Matter: " + MainGameStatsHolder.Currency.Balance.ToString();
            textData.GetComponent<Text>().text = "Data: " + MainGameStatsHolder.Currency.Data.ToString();
        }
        IEnumerator DataUnlockChecker()
        {
            for (; ; )
            {
                if (ProgressionController.GameStage >= 4)
                {
                    textData.SetActive(true);
                    StopCoroutine(DataUnlockChecker());
                }
                else
                {
                    textData.SetActive(false);
                }

                yield return new WaitForSeconds(1);
            }
        }
    }
}