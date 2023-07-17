using System;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static MeteorVoyager.Assets.Scripts.GameStatsNameSpace.GameStats;

namespace MeteorVoyager.Assets.Scripts.MonoBehaviours.UpgradesNS
{
    /// <summary>
    /// Must be used with Button / UpgradeButton component
    /// Provides automatic buying function
    /// </summary>
    public abstract class UpgradesButtonActions : MonoBehaviour
    {
        protected abstract int Value { get; set; }
        protected Func<int, InfiniteInteger> _formula;
        protected InfiniteInteger _cost;
        protected Action _onStart;

        public void Start()
        {
            AfterRelocation += Init;
            AfterRelocation += () => UpdateText(_formula(Value));
            Init();
            GetComponent<Button>().onClick.AddListener(Buy);
            UpdateText(_cost);
            _onStart?.Invoke();
        }
        protected void Init()
        {
            _formula = GetUpgradeFormula();
            _cost = _formula(Value);
        }
        protected abstract Func<int, InfiniteInteger> GetUpgradeFormula();
        public abstract InfiniteInteger Balance { get; set; }

        public bool CheckIfCanUpgrade()
        {
            _cost = _formula(Value);
            if (MainGameStatsHolder.Currency.Balance >= _cost && _cost != -1)
            {
                return true;
            }
            return false;
        }
        public void Buy()
        {
            void Upgrade(InfiniteInteger cost)
            {
                Value++;
                Balance -= cost;
            }
            if (BuyMultiplier.multiplier != -1)
            {
                for (int i = 0; i < BuyMultiplier.multiplier; i++)
                {
                    if (!CheckIfCanUpgrade())
                    {
                        break;
                    }
                    Upgrade(_cost);
                }
            }
            else
            {
                for (; ; )
                {
                    if (!CheckIfCanUpgrade())
                    {
                        break;
                    }
                    Upgrade(_cost);
                }
            }
            UpdateText(_formula(Value));
            GetComponent<Button>().interactable = _formula(Value) != -1;
        }

        public void UpdateText(InfiniteInteger cost)
        {
            var r = new Regex(@"
                (?<=[A-Z])(?=[A-Z][a-z]) |
                 (?<=[^A-Z])(?=[A-Z]) |
                 (?<=[A-Za-z])(?=[^A-Za-z])", RegexOptions.IgnorePatternWhitespace);
            var text = transform.GetChild(0).GetComponent<TextMeshProUGUI>();

            if (_formula(Value) != -1)
            {
                text.text =
                    $"{r.Replace(name, " ")}\n" +
                    $"Lvl: {Value}\n" +
                    $"Cost: {(cost == 0 ? "FREE" : cost)}";
            }
            else
            {
                text.text = $"MAX LVL ({Value})";
                GetComponent<Button>().interactable = false;
            }
        }
    }
}