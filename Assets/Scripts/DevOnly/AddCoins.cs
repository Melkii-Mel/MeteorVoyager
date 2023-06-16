using Assets.Scripts.GameStatsNameSpace;
using UnityEngine;
using UnityEngine.UI;

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
            Currency.Instance.Balance += (InfiniteInteger)balance;
        }
    }
}