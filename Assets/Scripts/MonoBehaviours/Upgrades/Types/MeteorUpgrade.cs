using static MeteorVoyager.Assets.Scripts.GameStatsNameSpace.MeteorUpgrades;
using System;
using UnityEngine;
using MeteorVoyager.Assets.Scripts.GameStatsNameSpace;

namespace MeteorVoyager.Assets.Scripts.MonoBehaviours.UpgradesNS.Types
{
    public class MeteorUpgrade : UpgradesButtonActions
    {
        [SerializeField] Upgrades _upgrade;
        protected override Func<int, InfiniteInteger> GetUpgradeFormula()
        {
            return Functions[(int)_upgrade];
        }

        protected override int GetUpgradeLvl()
        {
            return GameStats.MainGameStatsHolder.MeteorUpgrades.GetUpgradeLvl(_upgrade);
        }

        protected override void UpdateGameStats(int value, InfiniteInteger costOfUpgrade)
        {
            GameStats.MainGameStatsHolder.MeteorUpgrades.Upgrade(_upgrade, value);
            GameStats.MainGameStatsHolder.Currency.Balance -= costOfUpgrade;
        }
    }
}
