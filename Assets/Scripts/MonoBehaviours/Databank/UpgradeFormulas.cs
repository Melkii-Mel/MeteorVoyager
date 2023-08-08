using System;
using System.Collections.Generic;

namespace MonoBehaviours.DataBank
{
    public class UpgradeFormulas
    {
        public Func<int, InfiniteInteger> this[Upgrade key] => _functions[key];

        private readonly Dictionary<Upgrade, Func<int, InfiniteInteger>> _functions = new()
        {
            {
                Upgrade.AsteroidHealth,
                lvl => new InfiniteInteger(10).Pow(lvl)
            }
        };
    }
}