using static MeteorVoyager.Assets.Scripts.GameStatsNameSpace.DataUpgradesGameStats;
using UnityEngine;
using System;
using MeteorVoyager.Assets.Scripts.GameStatsNameSpace;

namespace MeteorVoyager.Assets.Scripts.MonoBehaviours.UpgradesNS.Types
{
    public class DataUpgrade : UpgradesButtonActions
    {
        [SerializeField] Upgrades _upgrade;

        public override InfiniteInteger Balance
        {
            get
            {
                return GameStats.MainGameStatsHolder.Currency.Data;
            }
            set
            {
                GameStats.MainGameStatsHolder.Currency.Data = value;
            }
        }

        protected override int Value
        {
            get
            {
                return GameStats.MainGameStatsHolder.DataUpgrades.GetUpgradeLvl(_upgrade);
            }
            set
            {
                GameStats.MainGameStatsHolder.DataUpgrades.Upgrade(_upgrade, value);
            }
        }

        protected override Func<int, InfiniteInteger> GetUpgradeFormula()
        {
            return Functions[(int)_upgrade];
        }
    }
}
