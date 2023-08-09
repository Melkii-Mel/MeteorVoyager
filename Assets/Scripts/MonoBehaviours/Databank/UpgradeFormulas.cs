using System;
using System.Collections.Generic;

namespace MonoBehaviours.DataBank
{
    public class UpgradeFormulas
    {
        public Func<int, InfiniteInteger> this[Upgrade key] => Functions[key];

        private static readonly Dictionary<Upgrade, Func<int, InfiniteInteger>> Functions = new()
        {
            {
                Upgrade.AsteroidHealth,
                lvl => new InfiniteInteger(10).Pow(lvl)
            },
            {
                Upgrade.Material0Unlock,
                _ => new InfiniteInteger(1, 3)
            },
            {
                Upgrade.Material1Unlock,
                _ => new InfiniteInteger(1, 9)
            },
            {
                Upgrade.Material2Unlock,
                _ => new InfiniteInteger(1, 20)
            },
            {
                Upgrade.Material3Unlock,
                _ => new InfiniteInteger(1, 50)
            },
            {
                Upgrade.Material4Unlock,
                _ => new InfiniteInteger(1, 100)
            },
            {
                Upgrade.InstantDamageUnlock,
                _ => new InfiniteInteger(1, 10)
            }
        };
    }
}