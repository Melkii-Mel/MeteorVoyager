using Codice.CM.Common.Tree.Partial;
using SerializationLibrary;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace MeteorVoyager.Assets.Scripts.GameStatsNameSpace
{
    public class TurretUpgrades : Serializable<TurretUpgrades>
    {
        public int PierceCount { get; set; } = 0;
        public int ChargeAttack { get; set; } = 0;
        public int Damage { get; set; } = 0;
        public int SpawnCooldown { get; set; } = 0;
        public int ShotCooldown { get; set; } = 0;

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
                _ => throw new ArgumentOutOfRangeException("Upgrade does not exist"),
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

        public static List<Func<int, InfiniteInteger>> Functions { get; } = new()
        {
            //PierceCount
            (int lvl) =>
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
            (int lvl) =>
            {
                if (lvl <= 25)
                {
                    return lvl == 0 ? 10000 : new InfiniteInteger(1, lvl + 2);
                }
                else
                {
                    return -1;
                }
            },
            //Damage
            (int lvl) =>
            {
                if (lvl <= 50000)
                {
                    return InfiniteInteger.Pow(30, Mathf.Pow(lvl/20f + 1, 0.23f));
                }
                else
                {
                    return -1;
                }
            },
            //SpawnCooldown
            (int lvl) =>
            {
                if (lvl <= 50000)
                {
                    return InfiniteInteger.Pow(5, Mathf.Pow(lvl, 0.23f));
                }
                else
                {
                    return -1;
                }
            },
            //ShotCooldown
            (int lvl) =>
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
