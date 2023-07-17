using static MeteorVoyager.Assets.Scripts.GameStatsNameSpace.MeteorUpgrades;
using System;
using UnityEngine;
using MeteorVoyager.Assets.Scripts.GameStatsNameSpace;

namespace MeteorVoyager.Assets.Scripts.MonoBehaviours.UpgradesNS.Types
{
    public class MeteorUpgrade : UpgradesButtonActions
    {
        [SerializeField] Upgrades _upgrade;

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
                return GameStats.MainGameStatsHolder.MeteorUpgrades.GetUpgradeLvl(_upgrade);
            }
            set
            {
                GameStats.MainGameStatsHolder.MeteorUpgrades.Upgrade(_upgrade, value);
            }
        }

        protected override Func<int, InfiniteInteger> GetUpgradeFormula()
        {
            return Functions[(int)_upgrade];
        }
    }
}
