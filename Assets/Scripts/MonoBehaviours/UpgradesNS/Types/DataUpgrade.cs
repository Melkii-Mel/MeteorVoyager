using System;
using GameStatsNS.GameStatsTypes.Upgrades;
using UnityEngine;
using UnityEngine.Serialization;
using static GameStatsNS.GameStats;

namespace MonoBehaviours.UpgradesNS.Types
{
    public class DataUpgrade : UpgradesButtonActions
    {
        [FormerlySerializedAs("_upgrade")] [SerializeField] private DataUpgradesGameStats.Upgrades upgrade;

        public override InfiniteInteger Balance
        {
            get
            {
                return MainGameStatsHolder.Currency.Data;
            }
            set
            {
                MainGameStatsHolder.Currency.Data = value;
            }
        }

        protected override int Value
        {
            get
            {
                return MainGameStatsHolder.DataUpgrades.GetUpgradeLvl(upgrade);
            }
            set
            {
                MainGameStatsHolder.DataUpgrades.Upgrade(upgrade, value);
            }
        }

        protected override Func<int, InfiniteInteger> GetUpgradeFormula()
        {
            return DataUpgradesGameStats.Functions[(int)upgrade];
        }
    }
}
