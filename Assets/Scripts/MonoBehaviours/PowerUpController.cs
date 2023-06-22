using static MeteorVoyager.Assets.Scripts.GameStatsNameSpace.GameStats;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace MeteorVoyager.Assets.Scripts.MonoBehaviours
{
    public class PowerUpController : MonoBehaviour
    {
        [SerializeField] private Text coinsText;
        [SerializeField] private Text powerText;
        [SerializeField] private Text explosionsText;
        private bool coinsRunning;
        private bool powerRunning;
        private bool explosionRunning;

        public void StartController()
        {
            if (MainGameStatsHolder.Timers.CoinMultiplierTimer > 0 && !coinsRunning)
            {
                StartCoroutine(CoinTimer());
            }
            if (MainGameStatsHolder.Timers.DamageMultiplierTimer > 0 && !powerRunning)
            {
                StartCoroutine(PowerTimer());
            }
            if (MainGameStatsHolder.Timers.ExplosivesAttacksTimer > 0 && !explosionRunning)
            {
                StartCoroutine(ExplosionsTimer());
            }
        }

        IEnumerator CoinTimer()
        {
            coinsRunning = true;
            while (MainGameStatsHolder.Timers.CoinMultiplierTimer > 0)
            {
                float coins = MainGameStatsHolder.Timers.CoinMultiplierTimer;
                coinsText.text = ConvertTimeToMinutesSeconds(coins);
                MainGameStatsHolder.Timers.CoinMultiplierTimer -= Time.deltaTime;
                yield return null;
            }
            coinsText.text = "00:00";
            coinsRunning = false;
        }

        IEnumerator PowerTimer()
        {
            powerRunning = true;
            while (MainGameStatsHolder.Timers.DamageMultiplierTimer > 0)
            {
                float damage = MainGameStatsHolder.Timers.DamageMultiplierTimer;
                powerText.text = ConvertTimeToMinutesSeconds(damage);
                MainGameStatsHolder.Timers.DamageMultiplierTimer -= Time.deltaTime;
                yield return null;
            }
            powerText.text = "00:00";
            powerRunning = false;
        }

        IEnumerator ExplosionsTimer()
        {
            explosionRunning = true;
            while (MainGameStatsHolder.Timers.ExplosivesAttacksTimer > 0)
            {
                float explosions = MainGameStatsHolder.Timers.ExplosivesAttacksTimer;
                explosionsText.text = ConvertTimeToMinutesSeconds(explosions);
                MainGameStatsHolder.Timers.ExplosivesAttacksTimer -= Time.deltaTime;
                yield return null;
            }
            explosionsText.text = "00:00";
            explosionRunning = false;
        }

        private string ConvertTimeToMinutesSeconds(float input)
        {
            int minutes = (int)(input - 0.01f) / 60;
            float seconds = input - 0.01f - minutes * 60;
            string result = $"{minutes}:{(int)seconds}";
            return result;
        }
    }
}