using System;
using static GameStatsNS.GameStats;

namespace MonoBehaviours
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

        private void CheckGameStage(float deltaTimeMS)
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

        private void UpdateGameStage()
        {
            GameStage++;
            _onUpdate();
        }
    }
}