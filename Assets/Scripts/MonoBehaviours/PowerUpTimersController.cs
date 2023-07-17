using static MeteorVoyager.Assets.Scripts.GameStatsNameSpace.GameStats;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace MeteorVoyager.Assets.Scripts.MonoBehaviours
{
    public class PowerUpTimersController : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI coinsText;
        [SerializeField] private TextMeshProUGUI powerText;
        [SerializeField] private TextMeshProUGUI explosionsText;

        public void Start()
        {
            GlobalTimer.OnTick += HandleTimers;
        }
        void HandleTimers(float deltaTimeMS)
        {
            CoinTimer();
            PowerTimer();
            ExplosionsTimer();
        }
        void CoinTimer()
        {
            string text = DEFAULT_TEXT;

            if (MainGameStatsHolder.Timers.CoinMultiplierTimer > 0)
            {
                float coins = MainGameStatsHolder.Timers.CoinMultiplierTimer;
                if (coins < 0) coins = 0;
                text = ToString(coins);
                MainGameStatsHolder.Timers.CoinMultiplierTimer -= Time.deltaTime;
            }
            coinsText.text = Texts.TimersTexts.CoinMultiplierTimerText + text;
        }

        void PowerTimer()
        {
            string text = DEFAULT_TEXT;

            if (MainGameStatsHolder.Timers.DamageMultiplierTimer > 0)
            {
                float damage = MainGameStatsHolder.Timers.DamageMultiplierTimer;
                if (damage < 0) damage = 0;
                text = ToString(damage);
                MainGameStatsHolder.Timers.DamageMultiplierTimer -= Time.deltaTime;
            }
            powerText.text = Texts.TimersTexts.DamageMultiplierTimerText + text;
        }

        void ExplosionsTimer()
        {
            string text = DEFAULT_TEXT;

            if (MainGameStatsHolder.Timers.ExplosivesAttacksTimer > 0)
            {
                float explosions = MainGameStatsHolder.Timers.ExplosivesAttacksTimer;
                if (explosions < 0) explosions = 0;
                text = ToString(explosions);
                MainGameStatsHolder.Timers.ExplosivesAttacksTimer -= Time.deltaTime;
            }
            explosionsText.text = Texts.TimersTexts.ExplosionsTimerText + text;
        }


        const string DEFAULT_TEXT = " -.--";
        private string ToString(float input)
        {
            return string.Format(" {0:f2}", input);
        }
    }
}