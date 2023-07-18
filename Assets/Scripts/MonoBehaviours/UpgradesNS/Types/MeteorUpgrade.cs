using System;
using GameStatsNS.GameStatsTypes.Upgrades;
using UnityEngine;
using UnityEngine.Serialization;
using static GameStatsNS.GameStats;

namespace MonoBehaviours.UpgradesNS.Types
{
    public class MeteorUpgrade : UpgradesButtonActions
    {
        [FormerlySerializedAs("_upgrade")] [SerializeField] private MeteorUpgrades.Upgrades upgrade;

        public override InfiniteInteger Balance
        {
            get => MainGameStatsHolder.Currency.Balance;
            set => MainGameStatsHolder.Currency.Balance = value;
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
