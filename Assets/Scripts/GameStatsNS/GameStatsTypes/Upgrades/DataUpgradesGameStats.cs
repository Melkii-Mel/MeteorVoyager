using System;
using System.Collections.Generic;
using SerializationLibrary;

namespace GameStatsNS.GameStatsTypes.Upgrades
{
    public class DataUpgradesGameStats : Serializable<DataUpgradesGameStats>
    {
        public int BossSpawnChanceLvl { get; set; }
        public int ForceFieldLvl { get; set; }
        public int UltracoinSpawnChanceLvl { get; set; }
        public int ScreenExplosionLvl { get; set; }
        public int MultishotLvl { get; set; }

        public enum Upgrades
        {
            BossSpawnChance,
            ForceField,
            UltracoinSpawnChance,
            ScreenExplosion,
            Multishot,
        }
        public static List<Func<int, InfiniteInteger>> Functions { get; } = new()
        {
            //BossSpawnChangeLvl
            lvl =>
            {
                if (lvl > 20)
                {
                    return -1;
                }
                return (int)MathF.Pow(10f, lvl);
            },
            //ForceFieldLvl
            lvl =>
            {
                if (lvl > 10000)
                {
                    return -1;
                }
                return (int)MathF.Pow(lvl + 10, 3);
            },
            //UltracoinSpawnChanceLvl
            lvl =>
            {
                if (lvl > 5)
                {
                    return -1;
                }
                return (int)MathF.Pow(100, lvl + 2);
            },
            //ScreenExplosionLvl
            lvl =>
            {
                if (lvl > 10000)
                {
                    return -1;
                }
                return (int)Math.Pow(lvl + 10, 3);
            },
            //MultishotLvl
            lvl =>
            {
                if (lvl > 9)
                {
                    return -1;
                }
                return (int)Math.Pow(1000, lvl + 1);
            }
        };

        public int GetUpgradeLvl(Upgrades upgrade)
        {
            return upgrade switch
            {
                Upgrades.BossSpawnChance => BossSpawnChanceLvl,
                Upgrades.ForceField => ForceFieldLvl,
                Upgrades.UltracoinSpawnChance => UltracoinSpawnChanceLvl,
                Upgrades.ScreenExplosion => ScreenExplosionLvl,
                Upgrades.Multishot => MultishotLvl,
                _ => throw new ArgumentOutOfRangeException(nameof(upgrade)),
            };
        }
        public void Upgrade(Upgrades upgrade, int value = -1)
        {
            switch (upgrade)
            {
                case Upgrades.BossSpawnChance:
                    BossSpawnChanceLvl = value;
                    break;
                case Upgrades.ForceField:
                    ForceFieldLvl = value;
                    break;
                case Upgrades.UltracoinSpawnChance:
                    UltracoinSpawnChanceLvl = value;
                    break;
                case Upgrades.ScreenExplosion:
                    ScreenExplosionLvl = value;
                    break;
                case Upgrades.Multishot:
                    MultishotLvl = value;
                    break;
            }
        }
    }
}
