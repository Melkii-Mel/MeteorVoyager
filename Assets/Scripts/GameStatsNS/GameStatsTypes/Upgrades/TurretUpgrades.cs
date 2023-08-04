using System;
using System.Collections.Generic;
using SerializationLibrary;
using UnityEngine;

namespace GameStatsNS.GameStatsTypes.Upgrades
{
    public class TurretUpgrades : Serializable<TurretUpgrades>
    {
        // ReSharper disable MemberCanBePrivate.Global
        public int PierceCount { get; set; }
        public int ChargeAttack { get; set; }
        public int Damage { get; set; }
        public int SpawnCooldown { get; set; }
        public int ShotCooldown { get; set; }

        // ReSharper restore MemberCanBePrivate.Global
        public void ResetDeletableValues()
        {
            PierceCount = 0;
            ChargeAttack = 0;
            Damage = 0;
            SpawnCooldown = 0;
            ShotCooldown = 0;
        }
        public enum Upgrades
        {
            PierceCount,
            ChargeAttack,
            Damage,
            SpawnCooldown,
            ShotCooldown,
        }

        public int GetUpgradeLvl(Upgrades upgrade)
        {
            return upgrade switch
            {
                Upgrades.PierceCount => PierceCount,
                Upgrades.ChargeAttack => ChargeAttack,
                Upgrades.Damage => Damage,
                Upgrades.SpawnCooldown => SpawnCooldown,
                Upgrades.ShotCooldown => ShotCooldown,
                _ => throw new ArgumentOutOfRangeException(nameof(upgrade), $"Upgrade ({upgrade.ToString()}) does not exist"),
            };
        }
        public void Upgrade(Upgrades upgrade, int value)
        {
            switch (upgrade)
            {
                case Upgrades.PierceCount:
                    PierceCount = value;
                    break;
                case Upgrades.ChargeAttack:
                    ChargeAttack = value;
                    break;
                case Upgrades.Damage:
                    Damage = value;
                    break;
                case Upgrades.SpawnCooldown:
                    SpawnCooldown = value;
                    break;
                case Upgrades.ShotCooldown:
                    ShotCooldown = value;
                    break;
            }
        }

        private static readonly InfiniteInteger SpawnCooldownConst0 = InfiniteInteger.Pow(10, 295);
        public static List<Func<int, InfiniteInteger>> Functions { get; } = new()
        {
            //PierceCount
            lvl =>
            {

                if (lvl <= 10)
                {
                    return InfiniteInteger.Pow(lvl + 2, 8);
                }
                else
                {
                    return -1;
                }
            },
            //ChargeAttack
            lvl =>
            {
                if (lvl <= 2500)
                {
                    return lvl == 0 ? 10000 : new InfiniteInteger(1, lvl + 2);
                }
                else
                {
                    return -1;
                }
            },
            //Damage
            lvl =>
            {
                if (lvl <= 50000)
                {
                    return InfiniteInteger.Pow(4, lvl / 10f) + 20;
                }
                else
                {
                    return -1;
                }
            },
            //SpawnCooldown
            lvl =>
            {
                if (lvl < 1000)
                {
                    return InfiniteInteger.Pow(lvl / 10, 3) + 10;;
                }
                else if (lvl < 50000)
                {
                    return InfiniteInteger.Pow(2, lvl) / SpawnCooldownConst0;
                }
                else
                {
                    return -1;
                }
            },
            //ShotCooldown
            lvl =>
            {
                if (lvl <= 200)
                {
                    return InfiniteInteger.Pow(lvl, 2.8f) * 40 / (lvl < 10 ? 10 : 1);
                }
                else
                {
                    return -1;
                }
            }
        };
    }
}
