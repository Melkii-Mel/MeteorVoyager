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

        private new void OnEnable()
        {
            base.OnEnable();
            if (upgradeEnum == TurretUpgrades.Upgrades.Damage)
            {
                Relocation.OnRelocationEnd += DamageUpgradeRelocationEnd;
            }
        }
        
        private new void OnDisable()
        {
            base.OnDisable();
            Relocation.OnRelocationEnd -= DamageUpgradeRelocationEnd;
        }
        
        private new void Start()
        {
            base.Start();
            if (upgradeEnum == TurretUpgrades.Upgrades.Damage)
            {
                StartDamageController();
            }
        }

        private void DamageUpgradeRelocationEnd(Relocation sender, Relocation.RelocationEventArgs args)
        {
            StartDamageController();
        }

        protected override InfiniteInteger Balance
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
            throw new ArgumentOutOfRangeException(nameof(upgradeEnum), 
                $"{upgradeEnum.ToString()} upgrade support is not implemented yet");
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

            yield return new WaitUntil(() => isActiveAndEnabled);
            SetActive(false);
            transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "LOCKED";
            if (MainGameStatsHolder.TurretUpgrades.SpawnCooldown < DAMAGE_UPGRADE_CONDITION_LVL)
            {
                yield return new WaitUntil(() => MainGameStatsHolder.TurretUpgrades.SpawnCooldown >= DAMAGE_UPGRADE_CONDITION_LVL);
            }
            SetActive(true);
            UpdateText(Cost);
        }

        protected override UpgradeEventArgs GetEventArgs()
        {
            return new(upgradeEnum, Value, this);
        }
    }
}