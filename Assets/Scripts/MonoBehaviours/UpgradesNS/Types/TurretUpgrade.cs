using System;
using System.Collections;
using GameStatsNS.GameStatsTypes.Upgrades;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using static GameStatsNS.GameStats;

namespace MonoBehaviours.UpgradesNS.Types
{
    public class TurretUpgrade : UpgradesButtonActions
    {
        [FormerlySerializedAs("_upgrade")] [SerializeField] private TurretUpgrades.Upgrades upgrade;

        public TurretUpgrade()
        {
            OnStart = StartDamageController;
        }

        public override InfiniteInteger Balance
        {
            get
            {
                return MainGameStatsHolder.Currency.Balance;
            }
            set
            {
                MainGameStatsHolder.Currency.Balance = value;
            }
        }

        protected override int Value
        {
            get
            {
                return MainGameStatsHolder.TurretUpgrades.GetUpgradeLvl(upgrade);
            }
            set
            {
                MainGameStatsHolder.TurretUpgrades.Upgrade(upgrade, value);
            }
        }

        protected override Func<int, InfiniteInteger> GetUpgradeFormula()
        {
            return TurretUpgrades.Functions[(int)upgrade];
        }
        protected void StartDamageController()
        {
            StartCoroutine(DamageUpgradeStateController());
        }

        private IEnumerator DamageUpgradeStateController()
        {
            void SetActive(bool state)
            {
                IsDamageUpgradeEnabled = state;
                GetComponent<Button>().interactable = state;
            }

            if (upgrade != TurretUpgrades.Upgrades.Damage)
            {
                yield break;
            }
            SetActive(false);
            transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "LOCKED";
            while (MainGameStatsHolder.TurretUpgrades.SpawnCooldown < 50)
            {
                yield return null;
            }
            SetActive(true);
            UpdateText(Cost);
        }
    }
}
