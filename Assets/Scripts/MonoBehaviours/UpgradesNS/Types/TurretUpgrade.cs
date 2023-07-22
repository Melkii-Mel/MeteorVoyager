using System;
using System.Collections;
using GameStatsNS.GameStatsTypes.Upgrades;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static GameStatsNS.GameStats;

namespace MonoBehaviours.UpgradesNS.Types
{
    public class TurretUpgrade : UpgradesButtonActions
    {
        private const int DAMAGE_UPGRADE_CONDITION_LVL = 50;
        [SerializeField] private TurretUpgrades.Upgrades upgrade;

        private new void Start()
        {
            base.Start();
            if (upgrade == TurretUpgrades.Upgrades.Damage)
            {
                StartDamageController();
                Relocation.OnRelocation += (_, _) => StartDamageController();
            }
        }
        public override InfiniteInteger Balance
        {
            get => MainGameStatsHolder.Currency.Balance;
            set => MainGameStatsHolder.Currency.Balance = value;
        }

        protected override int Value
        {
            get => MainGameStatsHolder.TurretUpgrades.GetUpgradeLvl(upgrade);
            set => MainGameStatsHolder.TurretUpgrades.Upgrade(upgrade, value);
        }

        protected override Func<int, InfiniteInteger> GetUpgradeFormula()
        {
            return TurretUpgrades.Functions[(int)upgrade];
        }

        private void StartDamageController()
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
            Debug.Log("falsed");
            SetActive(false);
            transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "LOCKED";
            yield return new WaitForSeconds(1);
            yield return new WaitUntil(() => MainGameStatsHolder.TurretUpgrades.SpawnCooldown >= DAMAGE_UPGRADE_CONDITION_LVL);
            Debug.Log("trued");
            SetActive(true);
            UpdateText(Cost);
        }
    }
}