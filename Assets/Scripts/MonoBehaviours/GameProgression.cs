using Assets.Scripts.GameStatsNameSpace;
using System.Collections;
using UnityEngine;

namespace MeteorVoyager.Assets.Scripts.MonoBehaviours
{
    public class GameProgression : MonoBehaviour
    {
        public static int gameStage;
        public static Progression progression { get; private set; }

        private void Awake()
        {
            progression = new GameStatsCreator().CreateGameStats<Progression>();
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
                if (gameStage == 1 && TurretUpgrades.Instance.ChargeAttack >= 1)
                {
                    UpdateGameStage();
                }
                if (gameStage == 2 && Currency.Instance.Balance >= InfiniteInteger.Million)
                {
                    UpdateGameStage();
                }
                if (gameStage == 3 && Currency.Instance.Data > 0)
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