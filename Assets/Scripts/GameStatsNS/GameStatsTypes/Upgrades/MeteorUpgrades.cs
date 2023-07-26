using System;
using System.Collections.Generic;
using SerializationLibrary;
using UnityEngine;

namespace GameStatsNS.GameStatsTypes.Upgrades
{
    public class MeteorUpgrades : Serializable<MeteorUpgrades>
    {
        public int CoinMultiplier { get; private set; }
        public int CoinMultiplierTimeUpgrade { get; private set; }
        public int DamageMultiplierTimeUpgrade { get; private set; }
        public int GlowingEnemiesSpawnRate { get; private set; }
        public int ExplosivesAttacksTimeUpgrade { get; private set; }

        public void ResetDeletableValues()
        {
            CoinMultiplier = 0;
            CoinMultiplierTimeUpgrade = 0;
            DamageMultiplierTimeUpgrade = 0;
            GlowingEnemiesSpawnRate = 0;
            ExplosivesAttacksTimeUpgrade = 0;
        }
        public enum Upgrades
        {
            CoinMultiplier,
            CoinMultiplierTimeUpgrade,
            DamageMultiplierTimeUpgrade,
            GlowingEnemiesSpawnRate,
            ExplosivesAttacksTimeUpgrade,
        }
        public int GetUpgradeLvl(Upgrades upgrade)
        {
            return upgrade switch
            {
                Upgrades.CoinMultiplierTimeUpgrade => CoinMultiplierTimeUpgrade,
                Upgrades.CoinMultiplier => CoinMultiplier,
                Upgrades.DamageMultiplierTimeUpgrade => DamageMultiplierTimeUpgrade,
                Upgrades.ExplosivesAttacksTimeUpgrade => ExplosivesAttacksTimeUpgrade,
                Upgrades.GlowingEnemiesSpawnRate => GlowingEnemiesSpawnRate,
                _ => throw new ArgumentOutOfRangeException(nameof(upgrade)),
            };
        }
        public void Upgrade(Upgrades upgrade, int value = -1)
        {
            switch (upgrade)
            {
                case Upgrades.CoinMultiplier:
                    CoinMultiplier = value; 
                    break;
                case Upgrades.CoinMultiplierTimeUpgrade:
                    CoinMultiplierTimeUpgrade = value; 
                    break;
                case Upgrades.DamageMultiplierTimeUpgrade:
                    DamageMultiplierTimeUpgrade = value;
                    break;
                case Upgrades.GlowingEnemiesSpawnRate:
                    GlowingEnemiesSpawnRate = value;
                    break;
                case Upgrades.ExplosivesAttacksTimeUpgrade:
                    ExplosivesAttacksTimeUpgrade = value;
                    break;
            }
        }

        public static List<Func<int, InfiniteInteger>> Functions { get; } = new()
        {
            //CoinMultiplier
            (int lvl) => lvl == 0 ? 1000 :
                lvl == 1 ? 1000000 :
                lvl == 2 ? 100000000 :
                (-1),
            //CoinMultiplierTimeUpgrade
            (int lvl) =>
            {
                if (lvl <= 5)
                {
                    return (InfiniteInteger)Mathf.Pow(10, lvl+2);
                }
                else
                {
                    return -1;
                }
            },
            //DamageMultiplierTimeUpgrade
            (int lvl) => (lvl <= 5) ? (int)Mathf.Pow(10, lvl+2) : (-1),
            //GlowingEnemiesSpawnRate
            (int lvl) => (lvl <= 100) ? (int)Mathf.Pow(8.48f , Mathf.Pow(lvl + 1, 1/2f)) : (-1),
            //ExplosivesAttacksTimeUpgrade
            (int lvl) => (lvl <= 5) ? (int)Mathf.Pow(10, lvl+2) : (-1),
        };
    }
}
