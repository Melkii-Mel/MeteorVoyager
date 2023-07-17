using static MeteorVoyager.Assets.Scripts.GameStatsNameSpace.GameStats;
using static MeteorVoyager.Assets.Scripts.GameStatsNameSpace.TurretUpgrades;
using MeteorVoyager.Assets.Scripts.GameStatsNameSpace;
using System.Collections;
using UnityEngine.UI;
using System;
using UnityEngine;
using TMPro;

namespace MeteorVoyager.Assets.Scripts.MonoBehaviours.UpgradesNS.Types
{
    public class TurretUpgrade : UpgradesButtonActions
    {
        [SerializeField] Upgrades _upgrade;

        public TurretUpgrade()
        {
            _onStart = StartDamageController;
        }

        public override InfiniteInteger Balance
        {
            get
            {
                return GameStats.MainGameStatsHolder.Currency.Balance;
            }
            set
            {
                GameStats.MainGameStatsHolder.Currency.Balance = value;
            }
        }

        protected override int Value
        {
            get
            {
                return GameStats.MainGameStatsHolder.TurretUpgrades.GetUpgradeLvl(_upgrade);
            }
            set
            {
                GameStats.MainGameStatsHolder.TurretUpgrades.Upgrade(_upgrade, value);
            }
        }

        protected override Func<int, InfiniteInteger> GetUpgradeFormula()
        {
            return Functions[(int)_upgrade];
        }
        protected void StartDamageController()
        {
            StartCoroutine(DamageUpgradeStateController());
        }

        IEnumerator DamageUpgradeStateController()
        {
            void SetActive(bool state)
            {
                IsDamageUpgradeEnabled = state;
                GetComponent<Button>().interactable = state;
            }

            if (_upgrade != Upgrades.Damage)
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
            UpdateText(_cost);
        }
    }
}
