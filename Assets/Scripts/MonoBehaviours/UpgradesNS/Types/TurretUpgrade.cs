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
        [SerializeField] private TurretUpgrades.Upgrades upgradeEnum;

        private void OnEnable()
        {
            if (upgradeEnum == TurretUpgrades.Upgrades.Damage)
            {
                StartDamageController();
            }
        }

        private new void Start()
        {
            base.Start();
            if (upgradeEnum == TurretUpgrades.Upgrades.Damage)
            {
                StartDamageController();
                Relocation.OnRelocationEnd += (_, _) => StartDamageController();
            }
        }
        public override InfiniteInteger Balance
        {
            get => MainGameStatsHolder.Currency.Balance;
            set => MainGameStatsHolder.Currency.Balance = value;
        }

        protected override int Value
        {
            get => MainGameStatsHolder.TurretUpgrades.GetUpgradeLvl(upgradeEnum);
            set => MainGameStatsHolder.TurretUpgrades.Upgrade(upgradeEnum, value);
        }

        protected override Func<int, InfiniteInteger> GetUpgradeFormula()
        {
            return TurretUpgrades.Functions[(int)upgradeEnum];
        }

        protected override string GetUpgradeName()
        {
            switch (upgradeEnum)
            {
                case TurretUpgrades.Upgrades.Damage: return Texts.ButtonTexts.Damage;
                case TurretUpgrades.Upgrades.SpawnCooldown: return Texts.ButtonTexts.SpawnCooldown;
                case TurretUpgrades.Upgrades.ChargeAttack: return Texts.ButtonTexts.ChargeAttack;
                case TurretUpgrades.Upgrades.PierceCount: return Texts.ButtonTexts.PierceCountUpgrade;
                case TurretUpgrades.Upgrades.ShotCooldown: return Texts.ButtonTexts.ShotCooldown;
            }

            throw new Exception("nO upgrade exception lol what is that");
        }

        private void StartDamageController()
        {
            if (isActiveAndEnabled)
            {
                StartCoroutine(DamageUpgradeStateController());
            }
        }

        private IEnumerator DamageUpgradeStateController()
        {
            void SetActive(bool state)
            {
                IsDamageUpgradeEnabled = state;
                GetComponent<Button>().interactable = state;
            }
            SetActive(false);
            transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "LOCKED";
            yield return new WaitUntil(() => MainGameStatsHolder.TurretUpgrades.SpawnCooldown >= DAMAGE_UPGRADE_CONDITION_LVL);
            SetActive(true);
            UpdateText(Cost);
        }

        protected override UpgradeEventArgs GetEventArgs()
        {
            return new(upgradeEnum, Value, this);
        }
    }
}