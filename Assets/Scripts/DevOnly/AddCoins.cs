using static MeteorVoyager.Assets.Scripts.GameStatsNameSpace.GameStats;
using UnityEngine;
using UnityEngine.UI;
using MeteorVoyager.Assets.Scripts.GameStatsNameSpace;

namespace MeteorVoyager.Assets.Scripts.DevOnly
{
    public class AddCoins : MonoBehaviour
    {
        [SerializeField] private int balance;
        void Start()
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