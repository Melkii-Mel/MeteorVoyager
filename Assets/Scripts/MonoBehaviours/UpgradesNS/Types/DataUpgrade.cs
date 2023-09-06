using System;
using GameStatsNS.GameStatsTypes.Upgrades;
using UnityEngine;
using static GameStatsNS.GameStats;

namespace MonoBehaviours.UpgradesNS.Types
{
    public class DataUpgrade : UpgradesButtonActions
    {
        [SerializeField] private DataUpgradesGameStats.Upgrades upgradeEnum;

        protected override InfiniteInteger Balance
        {
            get => MainGameStatsHolder.Currency.Data;
            set => MainGameStatsHolder.Currency.Data = value;
        }


        protected override string GetUpgradeName()
        {
            switch (upgradeEnum)
            {
                case DataUpgradesGameStats.Upgrades.Multishot: return Texts.ButtonTexts.Multishot;
                case DataUpgradesGameStats.Upgrades.ForceField: return Texts.ButtonTexts.ForceField;
                case DataUpgradesGameStats.Upgrades.ScreenExplosion: return Texts.ButtonTexts.ScreenExplosion;
                case DataUpgradesGameStats.Upgrades.BossSpawnChance: return Texts.ButtonTexts.BossSpawnChance;
                case DataUpgradesGameStats.Upgrades.UltracoinSpawnChance: return Texts.ButtonTexts.UltracoinSpawnChance;
            }

            throw new Exception("nO exception Data upgrade");
        }

        protected override UpgradeEventArgs GetEventArgs()
        {
            return new(upgradeEnum, Value, this);
        }

        protected override int Value
        {
            get => MainGameStatsHolder.DataUpgrades.GetUpgradeLvl(upgradeEnum);
            set => MainGameStatsHolder.DataUpgrades.Upgrade(upgradeEnum, value);
        }

        protected override Func<int, InfiniteInteger> GetUpgradeFormula()
        {
            return DataUpgradesGameStats.Functions[(int)upgradeEnum];
        }
    }
}
