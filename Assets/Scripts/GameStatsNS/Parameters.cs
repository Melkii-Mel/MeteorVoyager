using System;
using GameStatsNS.GameStatsTypes.Upgrades;
using MonoBehaviours.UpgradesNS.Types;
using UnityEngine;

namespace GameStatsNS
{
    public class Parameters
    {
        public Parameters()
        {
            TurretUpgrade.OnUpgrade += args =>
            {
                if (args.UpgradeEnum.Equals(TurretUpgrades.Upgrades.Damage))
                {
                    SetDamage(args.UpgradeValue);
                    SetChargedAttackDamageAndPiecing(GameStats.MainGameStatsHolder.TurretUpgrades.ChargeAttack, GameStats.MainGameStatsHolder.TurretUpgrades.PierceCount);
                }
                if (args.UpgradeEnum.Equals(TurretUpgrades.Upgrades.PierceCount))
                {
                    SetChargedAttackDamageAndPiecing(GameStats.MainGameStatsHolder.TurretUpgrades.ChargeAttack, args.UpgradeValue);
                }
                if (args.UpgradeEnum.Equals(TurretUpgrades.Upgrades.SpawnCooldown))
                {
                    SetSpawnDelayAndHealth(args.UpgradeValue);
                }
                if (args.UpgradeEnum.Equals(TurretUpgrades.Upgrades.ShotCooldown))
                {
                    SetShotDelay(args.UpgradeValue);
                }
                if (args.UpgradeEnum.Equals(TurretUpgrades.Upgrades.ChargeAttack))
                {
                    SetChargedAttackDamageAndPiecing(args.UpgradeValue, GameStats.MainGameStatsHolder.TurretUpgrades.PierceCount);
                }
            };
            SetDamage(GameStats.MainGameStatsHolder.TurretUpgrades.Damage);
            SetSpawnDelayAndHealth(GameStats.MainGameStatsHolder.TurretUpgrades.SpawnCooldown);
            SetShotDelay(GameStats.MainGameStatsHolder.TurretUpgrades.ShotCooldown);
            SetChargedAttackDamageAndPiecing(GameStats.MainGameStatsHolder.TurretUpgrades.ChargeAttack, GameStats.MainGameStatsHolder.TurretUpgrades.PierceCount);
        }
        
        #region DamageParameter
        
        private InfiniteInteger _damage;
        public InfiniteInteger Damage => _damage;

        private void SetDamage(int lvl)
        {
            // ReSharper disable once PossibleLossOfFraction
            _damage = lvl * 2 * InfiniteInteger.Pow(2, lvl / 25);
        }
        
        #endregion

        #region SpawnrateParameter && HealthParameter

        private const float MIN_SPAWN_DELAY = 0.50f;
        private float _spawnDelay;
        public float SpawnDelay => _spawnDelay;

        private InfiniteInteger _health;
        public InfiniteInteger Health => _health;
        
        private void SetSpawnDelayAndHealth(int lvl)
        {
            _spawnDelay = 10f / ((Mathf.Log(lvl + 5, 2)) * (Mathf.Sqrt((lvl % 20 / 20f) + 1)) *
                                 (Mathf.Pow(lvl + 1, 1 / 5f)));
            if (_spawnDelay < MIN_SPAWN_DELAY) _spawnDelay = MIN_SPAWN_DELAY;
            // ReSharper disable once PossibleLossOfFraction
            _health = InfiniteInteger.Pow(2, lvl / 20);
        }
        
        #endregion

        #region ShotCooldown

        private float _shotDelay;
        public float ShotDelay => _shotDelay;

        private void SetShotDelay(int lvl)
        {
            _shotDelay = 2 / Mathf.Log(Mathf.Pow(lvl, 2) + 3);
        }
        
        #endregion

        #region Charged Attack Damage &U& Piercing

        private InfiniteInteger _chargedAttackDamage;
        public InfiniteInteger ChargedAttackDamage => _chargedAttackDamage;

        private int _chargedAttackPiercing;
        public int ChargedAttackPiercing => _chargedAttackPiercing;

        private void SetChargedAttackDamageAndPiecing(int chargedAttackLvl, int pierceCountLvl)
        {
            _chargedAttackDamage = InfiniteInteger.Pow(Damage, 1 + chargedAttackLvl / 10f) * 10;
            _chargedAttackPiercing = pierceCountLvl * chargedAttackLvl;
        }

        #endregion
    }
}