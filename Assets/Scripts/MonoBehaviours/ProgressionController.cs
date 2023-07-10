using static MeteorVoyager.Assets.Scripts.GameStatsNameSpace.GameStats;
using System.Collections;
using UnityEngine;
using MeteorVoyager.Assets.Scripts.GameStatsNameSpace;
using System;

namespace MeteorVoyager.Assets.Scripts.MonoBehaviours
{
    public class ProgressionController
    {
        public int GameStage { get; set; }
        private Action _onUpdate;
        public ProgressionController(Action action)
        {
            new Timer(0.1f, CheckGameStage, true);
            GameStage = 0;
            _onUpdate = action;
            action();
        }
        void CheckGameStage()
        {
            if (MainGameStatsHolder == null) return;
            if (GameStage == 0 && MainGameStatsHolder.TurretUpgrades.SpawnCooldown >= 50)
            {
                UpdateGameStage();
            }
            if (GameStage == 1 && MainGameStatsHolder.TurretUpgrades.ChargeAttack >= 1)
            {
                UpdateGameStage();
            }
            if (GameStage == 2 && MainGameStatsHolder.Currency.Balance >= InfiniteInteger.Million)
            {
                UpdateGameStage();
            }
            if (GameStage == 3 && MainGameStatsHolder.Currency.Data > 0)
            {
                UpdateGameStage();
            }
        }
        void UpdateGameStage()
        {
            GameStage++;
            _onUpdate();
        }
    }
}