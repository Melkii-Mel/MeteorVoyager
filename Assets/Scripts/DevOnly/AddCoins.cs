using UnityEngine;
using UnityEngine.UI;
using static GameStatsNS.GameStats;


namespace DevOnly
{
    public class AddCoins : MonoBehaviour
    {
        [SerializeField] private int balance;

        private void Start()
        {
            balance = balance == 0 ? 1000 : balance;
            transform.GetChild(0).gameObject.GetComponent<Text>().text = $"Give {balance} coins";
        }

        public void GiveMoney()
        {
            MainGameStatsHolder.Currency.Balance += (InfiniteInteger)balance;
        }
    }
}