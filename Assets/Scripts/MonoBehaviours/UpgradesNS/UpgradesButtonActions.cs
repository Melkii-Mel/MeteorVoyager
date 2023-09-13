using System;
using System.Threading.Tasks;
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
        
        /// <summary>
        /// Represents the amount of lvl ups that was made during the last upgrade
        /// </summary>
        protected int LastAmount { get; set; }
        private Func<int, InfiniteInteger> _formula;
        protected InfiniteInteger Cost => _formula(Value);
        
        #region events
        public delegate void UpgradeEventHandler(UpgradeEventArgs args);

        public static event UpgradeEventHandler OnUpgrade;
        
        #endregion

        protected void OnDisable()
        {
            Relocation.OnRelocationEnd -= RelocationEnd;
            GetComponent<Button>().onClick.RemoveListener(ButtonAction);
        }

        protected void OnEnable()
        {
            Relocation.OnRelocationEnd += RelocationEnd;
            GetComponent<Button>().onClick.AddListener(ButtonAction);
        }

        protected void Start()
        {
            Init();
            UpdateText(Cost);
        }

        private async void ButtonAction()
        {
            await Buy();
        }

        private void RelocationEnd(Relocation sender, Relocation.RelocationEventArgs args)
        {
            Init();
            UpdateText(_formula(Value));
        }
        private void Init()
        {
            _formula = GetUpgradeFormula();
            GetComponent<Button>().interactable = Cost != -1;
        }
        protected abstract Func<int, InfiniteInteger> GetUpgradeFormula();
        protected abstract InfiniteInteger Balance { get; set; }

        public bool CheckIfCanUpgrade()
        {
            return Balance >= Cost && Cost != -1;
        }
        public async Task Buy()
        {
            if (!CheckIfCanUpgrade()) return;
            LastAmount = 0;
            const int msInS = 1000;
            int msPerTick =  msInS / Application.targetFrameRate;
            DateTime current = DateTime.Now;
            async Task Upgrade(InfiniteInteger cost)
            {
                Balance -= cost;
                Value++;
                LastAmount++;
                if ((DateTime.Now - current).TotalMilliseconds >= msPerTick)
                {
                    current = DateTime.Now;
                    UpdateText(Cost);
                    await Task.Delay(1);
                    OnUpgrade?.Invoke(GetEventArgs());
                }
            }
            if (BuyMultiplier.Multiplier != -1)
            {
                for (int i = 0; i < BuyMultiplier.Multiplier; i++)
                {
                    if (!CheckIfCanUpgrade())
                    {
                        break;
                    }
                    await Upgrade(Cost);
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
                    await Upgrade(Cost);
                }
            }
            OnUpgrade?.Invoke(GetEventArgs());
            UpdateText(Cost);
            GetComponent<Button>().interactable = Cost != -1;
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
        protected abstract UpgradeEventArgs GetEventArgs();
    }
}