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
        private const int MAX_UPGRADES_CHUNK_SIZE = 1000;
        protected abstract int Value { get; set; }
        protected Func<int, InfiniteInteger> Formula;
        protected InfiniteInteger Cost => Formula(Value);

        public void Start()
        {
            Relocation.OnRelocationEnd += (_, _) => Init();
            Relocation.OnRelocationEnd += (_, _) => UpdateText(Formula(Value));
            Init();
            GetComponent<Button>().onClick.AddListener(async () => await Buy());
            UpdateText(Cost);
        }
        protected void Init()
        {
            Formula = GetUpgradeFormula();
            GetComponent<Button>().interactable = Cost != -1;
        }
        protected abstract Func<int, InfiniteInteger> GetUpgradeFormula();
        public abstract InfiniteInteger Balance { get; set; }

        public bool CheckIfCanUpgrade()
        {
            if (Balance >= Cost && Cost != -1)
            {
                return true;
            }
            return false;
        }
        public async Task Buy()
        {
            int upgradesCounter = 0;
            async Task Upgrade(InfiniteInteger cost)
            {
                upgradesCounter++;
                Value++;
                Balance -= cost;
                if (upgradesCounter > MAX_UPGRADES_CHUNK_SIZE)
                {
                    upgradesCounter = 0;
                    UpdateText(Formula(Value));
                    await Task.Delay(1);
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
            UpdateText(Cost);
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