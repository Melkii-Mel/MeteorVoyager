using static MeteorVoyager.Assets.Scripts.GameStatsNameSpace.GameStats;
using static MeteorVoyager.Assets.Scripts.GameStatsNameSpace.TurretUpgrades;
using MeteorVoyager.Assets.Scripts.GameStatsNameSpace;
using System.Collections;
using UnityEngine.UI;
using System;
using UnityEngine;

namespace MeteorVoyager.Assets.Scripts.MonoBehaviours.UpgradesNS.Types
{
    public class TurretUpgrade : UpgradesButtonActions
    {
        [SerializeField] Upgrades _upgrade;

        public TurretUpgrade()
        {
            _onStart = StartDamageController;
        }
        protected override Func<int, InfiniteInteger> GetUpgradeFormula()
        {
            return Functions[(int)_upgrade];
        }
        protected override int GetUpgradeLvl()
        {
            return MainGameStatsHolder.TurretUpgrades.GetUpgradeLvl(_upgrade);
        }
        protected override void UpdateGameStats(int value, InfiniteInteger costOfUpgrade)
        {
            MainGameStatsHolder.TurretUpgrades.Upgrade(_upgrade, value);
            MainGameStatsHolder.Currency.Balance -= costOfUpgrade;
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
            transform.GetChild(0).gameObject.GetComponent<Text>().text = "LOCKED";
            while (MainGameStatsHolder.TurretUpgrades.SpawnCooldown < 50)
            {
                yield return null;
            }
            SetActive(true);
            UpdateText(_cost);
        }
    }
}
