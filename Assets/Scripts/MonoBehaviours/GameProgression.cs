using static MeteorVoyager.Assets.Scripts.GameStatsNameSpace.GameStats;
using System.Collections;
using UnityEngine;
using MeteorVoyager.Assets.Scripts.GameStatsNameSpace;

namespace MeteorVoyager.Assets.Scripts.MonoBehaviours
{
    public class GameProgression : MonoBehaviour
    {
        public static int gameStage;
        public static Progression progression { get; private set; }

        private void Awake()
        {
            progression = MainGameStatsHolder.Progression;
            StartCoroutine(CheckGameStage());
        }

        IEnumerator CheckGameStage()
        {
            for (; ; )
            {
                if (gameStage == 0 && progression.IsDamageUpgradeEnabled)
                {
                    UpdateGameStage();
                }
                if (gameStage == 1 && MainGameStatsHolder.TurretUpgrades.ChargeAttack >= 1)
                {
                    UpdateGameStage();
                }
                if (gameStage == 2 && MainGameStatsHolder.Currency.Balance >= InfiniteInteger.Million)
                {
                    UpdateGameStage();
                }
                if (gameStage == 3 && MainGameStatsHolder.Currency.Data > 0)
                {
                    UpdateGameStage();
                }

                yield return null;
            }
        }

        void LoadGameStage()
        {
            gameStage = progression.GameStage;
        }

        void UpdateGameStage()
        {
            gameStage++;

        }
    }
}