using MeteorVoyager.Assets.Scripts;
using MeteorVoyager.Assets.Scripts.GameStatsNameSpace;
using MeteorVoyager.Assets.Scripts.Serialization;
using MeteorVoyager.Assets.Scripts.Serialization.Interfaces;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.GameStatsNameSpace
{
    [Serializable]
    public class TurretUpgrades : IGameStats, IDeletableOnRelocation
    {
        public static TurretUpgrades Instance = new();
        public int PierceCount { get; set; } = 0;
        public int ChargeAttack { get; set; } = 0;
        public int Damage { get; set; } = 0;
        public int SpawnCooldown { get; set; } = 0;
        public int ShotCooldown { get; set; } = 0;

        public string FileName { get; set; } = nameof(TurretUpgrades);

        TurretUpgrades()
        {
            new DataLoaderSaver<IGameStats>().LoadGameStat(this);
            GameStats.AppendIGameStats(this);
        }

        public void AddToAutoSaver()
        {
            AutoSaver<IGameStats>.GameStats.Add(this);
        }

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

        public static List<Func<int, InfiniteInteger>> Functions { get; } = new()
        {
            //PierceCount
            (int lvl) =>
            {

                if (lvl <= 10)
                {
                    return ((InfiniteInteger)(lvl + 2)).Pow(8);
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
                    return lvl == 0 ? 10000 : (InfiniteInteger)(lvl * Mathf.Pow(10, Mathf.Sqrt(lvl)));
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
                    return (InfiniteInteger)Mathf.Pow(30, Mathf.Pow(lvl/20f + 1, 0.23f));
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
                    return (InfiniteInteger)Mathf.Pow(5, Mathf.Pow(lvl, 0.23f));
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
                    return (InfiniteInteger)Mathf.Pow(lvl, 2.8f) * 40 / (lvl < 10 ? 10 : 1);
                }
                else
                {
                    return -1;
                }
            }
        };
    }
}
