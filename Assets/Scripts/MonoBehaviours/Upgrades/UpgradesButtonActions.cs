using static MeteorVoyager.Assets.Scripts.GameStatsNameSpace.GameStats;
using MeteorVoyager.Assets.Scripts.GameStatsNameSpace;
using System;
using System.Collections;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

namespace MeteorVoyager.Assets.Scripts.MonoBehaviours.UpgradesNS
{
    public abstract class UpgradesButtonActions : MonoBehaviour
    {
        protected int _value;
        protected Func<int, InfiniteInteger> _formula;
        protected InfiniteInteger _cost;
        protected Action _onStart;

        private void OnEnable()
        {
            OnRelocation += Start;
        }

        private void OnDisable()
        {
            OnRelocation -= Start;
        }
        public void Start()
        {
            Init();
            GetComponent<Button>().onClick.AddListener(Buy);
            UpdateText(_cost);
            _onStart?.Invoke();
        }
        protected void Init()
        {
            _formula = GetUpgradeFormula();
            _value = GetUpgradeLvl();
            _cost = _formula(_value);
        }
        protected abstract Func<int, InfiniteInteger> GetUpgradeFormula();
        protected abstract int GetUpgradeLvl();
        protected abstract void UpdateGameStats(int value, InfiniteInteger costOfUpgrade);

        public void Buy()
        {
            bool CheckIfCanUpgrade()
            {
                _cost = _formula(_value);
                if (MainGameStatsHolder.Currency.Balance < _cost || _cost == -1)
                {
                    return false;
                }
                return true;
            }
            void Upgrade(InfiniteInteger cost)
            {
                _value++;
                UpdateGameStats(_value, cost);
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
            UpdateText(_formula(_value));
            GetComponent<Button>().interactable = _formula(_value) != -1;
        }

        public void UpdateText(InfiniteInteger cost)
        {
            var r = new Regex(@"
                (?<=[A-Z])(?=[A-Z][a-z]) |
                 (?<=[^A-Z])(?=[A-Z]) |
                 (?<=[A-Za-z])(?=[^A-Za-z])", RegexOptions.IgnorePatternWhitespace);
            var text = transform.GetChild(0).GetComponent<Text>();

            if (_formula(_value) != -1)
            {
                text.text =
                    $"{r.Replace(name, " ")}\n" +
                    $"Lvl: {_value}\n" +
                    $"Cost: {(cost == 0 ? "FREE" : cost)}";
            }
            else
            {
                text.text = $"MAX LVL ({_value})";
                GetComponent<Button>().interactable = false;
            }
        }
    }
}