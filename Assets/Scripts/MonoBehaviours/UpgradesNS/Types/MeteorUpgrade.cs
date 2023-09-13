using System;
using GameStatsNS.GameStatsTypes.Upgrades;
using UnityEngine;
using static GameStatsNS.GameStats;

namespace MonoBehaviours.UpgradesNS.Types
{
    public class MeteorUpgrade : UpgradesButtonActions
    {
        [SerializeField] private MeteorUpgrades.Upgrades upgrade;

        protected override InfiniteInteger Balance
        {
            get => MainGameStatsHolder.Currency.Balance;
            set => MainGameStatsHolder.Currency.Balance = value;
        }

        protected override string GetUpgradeName()
        {
            switch (upgrade)
            {
                case MeteorUpgrades.Upgrades.CoinMultiplier:
                    return Texts.ButtonTexts.CoinMultiplier;
                case MeteorUpgrades.Upgrades.CoinMultiplierTimeUpgrade:
                    return Texts.ButtonTexts.CoinMultiplierTimeUpgrade;
                case MeteorUpgrades.Upgrades.DamageMultiplierTimeUpgrade:
                    return Texts.ButtonTexts.DamageMultiplierTimeUpgrade;
                case MeteorUpgrades.Upgrades.ExplosivesAttacksTimeUpgrade:
                    return Texts.ButtonTexts.ExplosivesAttacksTimeUpgrade;
                case MeteorUpgrades.Upgrades.GlowingEnemiesSpawnRate:
                    return Texts.ButtonTexts.GlowingEnemiesSpawnRate;
            }

            throw new Exception("nO exception Meteor upgrade");
        }

        protected override UpgradeEventArgs GetEventArgs()
        {
            return new(upgrade, Value, LastAmount, this);
        }

        protected override int Value
        {
            get => MainGameStatsHolder.MeteorUpgrades.GetUpgradeLvl(upgrade);
            set => MainGameStatsHolder.MeteorUpgrades.Upgrade(upgrade, value);
        }

        protected override Func<int, InfiniteInteger> GetUpgradeFormula()
        {
            return MeteorUpgrades.Functions[(int)upgrade];
        }
    }
}
