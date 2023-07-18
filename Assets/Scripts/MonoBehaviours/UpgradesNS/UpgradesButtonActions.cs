using System;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static GameStatsNS.GameStats;

namespace MonoBehaviours.UpgradesNS
{
    /// <summary>
    /// Must be used with Button / UpgradeButton component
    /// Provides automatic buying function
    /// </summary>
    public abstract class UpgradesButtonActions : MonoBehaviour
    {
        protected abstract int Value { get; set; }
        protected Func<int, InfiniteInteger> Formula;
        protected InfiniteInteger Cost;
        protected Action OnStart;

        public void Start()
        {
            AfterRelocation += Init;
            AfterRelocation += () => UpdateText(Formula(Value));
            Init();
            GetComponent<Button>().onClick.AddListener(Buy);
            UpdateText(Cost);
            OnStart?.Invoke();
        }
        protected void Init()
        {
            Formula = GetUpgradeFormula();
            Cost = Formula(Value);
        }
        protected abstract Func<int, InfiniteInteger> GetUpgradeFormula();
        public abstract InfiniteInteger Balance { get; set; }

        public bool CheckIfCanUpgrade()
        {
            Cost = Formula(Value);
            if (MainGameStatsHolder.Currency.Balance >= Cost && Cost != -1)
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
            if (BuyMultiplier.Multiplier != -1)
            {
                for (int i = 0; i < BuyMultiplier.Multiplier; i++)
                {
                    if (!CheckIfCanUpgrade())
                    {
                        break;
                    }
                    Upgrade(Cost);
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
                    Upgrade(Cost);
                }
            }
            UpdateText(Formula(Value));
            GetComponent<Button>().interactable = Formula(Value) != -1;
        }

        public void UpdateText(InfiniteInteger cost)
        {
            var r = new Regex(@"
                (?<=[A-Z])(?=[A-Z][a-z]) |
                 (?<=[^A-Z])(?=[A-Z]) |
                 (?<=[A-Za-z])(?=[^A-Za-z])", RegexOptions.IgnorePatternWhitespace);
            var text = transform.GetChild(0).GetComponent<TextMeshProUGUI>();

            if (Formula(Value) != -1)
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