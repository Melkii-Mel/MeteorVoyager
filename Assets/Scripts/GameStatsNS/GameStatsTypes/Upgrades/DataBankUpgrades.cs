using System;
using System.Collections.Generic;
using System.Linq;
using MonoBehaviours.DataBank;
using SerializationLibrary;

namespace GameStatsNS.GameStatsTypes.Upgrades
{
    public class DataBankUpgrades : Serializable<DataBankUpgrades>
    {
        public int this[Upgrade upgrade]
        {
            get
            {
                return Upgrades.First(u => u.upgrade == upgrade).value;
            }
            set
            {
                Upgrades.First(u => u.upgrade == upgrade).value = value;
            }
        }

        // ReSharper disable once FieldCanBeMadeReadOnly.Global
        // ReSharper disable once MemberCanBePrivate.Global
        public Pair[] Upgrades = new[]
        {
            new Pair(Upgrade.AsteroidHealth),
            new Pair(Upgrade.Material0Unlock),
            new Pair(Upgrade.Material1Unlock),
            new Pair(Upgrade.Material2Unlock),
            new Pair(Upgrade.Material3Unlock),
            new Pair(Upgrade.Material4Unlock),
            new Pair(Upgrade.InstantDamageUnlock)
        };
            
        [Serializable]
        public class Pair
        {
            public Upgrade upgrade;
            public int value;

            public Pair(Upgrade upgrade)
            {
                this.upgrade = upgrade;
                this.value = 0;
            }
            public Pair() { }
        }
    }
}