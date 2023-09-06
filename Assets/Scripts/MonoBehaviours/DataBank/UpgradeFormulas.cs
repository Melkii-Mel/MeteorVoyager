using System;
using System.Collections.Generic;
using MonoBehaviours.DataBank.Enums;

namespace MonoBehaviours.DataBank
{
    public class UpgradeFormulas
    {
        public Func<int, InfiniteInteger> this[UpgradeEnum key] => Functions[key];

        private static readonly Dictionary<UpgradeEnum, Func<int, InfiniteInteger>> Functions = new()
        {
            {
                UpgradeEnum.AsteroidHealth,
                lvl => new InfiniteInteger(10).Pow(lvl)
            },
            {
                UpgradeEnum.Material0Unlock,
                _ => new InfiniteInteger(1, 3)
            },
            {
                UpgradeEnum.Material1Unlock,
                _ => new InfiniteInteger(1, 9)
            },
            {
                UpgradeEnum.Material2Unlock,
                _ => new InfiniteInteger(1, 20)
            },
            {
                UpgradeEnum.Material3Unlock,
                _ => new InfiniteInteger(1, 50)
            },
            {
                UpgradeEnum.Material4Unlock,
                _ => new InfiniteInteger(1, 100)
            },
            {
                UpgradeEnum.InstantDamageUnlock,
                _ => new InfiniteInteger(1, 10)
            }
        };
    }
}