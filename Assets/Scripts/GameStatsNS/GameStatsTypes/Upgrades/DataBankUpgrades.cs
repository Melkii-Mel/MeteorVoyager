using System;
using System.Linq;
using MonoBehaviours.DataBank.Enums;
using SerializationLibrary;

namespace GameStatsNS.GameStatsTypes.Upgrades
{
    public class DataBankUpgrades : Serializable<DataBankUpgrades>
    {
        public int this[UpgradeEnum upgradeEnum]
        {
            get
            {
                return Upgrades.First(u => u.upgradeEnum == upgradeEnum).value;
            }
            set
            {
                Upgrades.First(u => u.upgradeEnum == upgradeEnum).value = value;
            }
        }

        // ReSharper disable once FieldCanBeMadeReadOnly.Global
        // ReSharper disable once MemberCanBePrivate.Global
        public Pair[] Upgrades = new[]
        {
            new Pair(UpgradeEnum.AsteroidHealth),
            new Pair(UpgradeEnum.Material0Unlock),
            new Pair(UpgradeEnum.Material1Unlock),
            new Pair(UpgradeEnum.Material2Unlock),
            new Pair(UpgradeEnum.Material3Unlock),
            new Pair(UpgradeEnum.Material4Unlock),
            new Pair(UpgradeEnum.InstantDamageUnlock)
        };
            
        [Serializable]
        public class Pair
        {
            public UpgradeEnum upgradeEnum;
            public int value;

            public Pair(UpgradeEnum upgradeEnum)
            {
                this.upgradeEnum = upgradeEnum;
                this.value = 0;
            }
            public Pair() { }
        }
    }
}