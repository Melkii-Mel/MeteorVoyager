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
        public void Start()
        {
            AfterRelocation += Init;
            AfterRelocation += () => UpdateText(Formula(Value));
            Init();
            GetComponent<Button>().onClick.AddListener(Buy);
            UpdateText(Cost);
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
            var text = transform.GetChild(0).GetComponent<TextMeshProUGUI>();

            if (cost != -1)
            {
                text.text =
                    $"{GetUpgradeName()}\n" +
                    $"Lvl: {Value}\n" +
                    $"{Texts.OtherTexts.Cost}: {(cost == 0 ? "FREE" : cost)}";
            }
            else
            {
                text.text = $"{GetUpgradeName()}\n" +
                    $"MAX LVL ({Value})";
                GetComponent<Button>().interactable = false;
            }
        }

        protected abstract string GetUpgradeName();
    }
}