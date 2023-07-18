using UnityEngine;
using UnityEngine.UI;
using static GameStatsNS.GameStats;

namespace MonoBehaviours
{
    public class PowerUpController : MonoBehaviour
    {
        [SerializeField] private Text coinsText;
        [SerializeField] private Text powerText;
        [SerializeField] private Text explosionsText;

        public void StartController()
        {
            GlobalTimer.OnTick += HandleTimers;
        }

        private void HandleTimers(float deltaTimeMS)
        {
            CoinTimer();
            PowerTimer();
            ExplosionsTimer();
        }

        private void CoinTimer()
        {
            while (MainGameStatsHolder.Timers.CoinMultiplierTimer > 0)
            {
                float coins = MainGameStatsHolder.Timers.CoinMultiplierTimer;
                if (coins < 0) coins = 0;
                coinsText.text = ConvertTimeToMinutesSeconds(coins);
                MainGameStatsHolder.Timers.CoinMultiplierTimer -= Time.deltaTime;
            }
            coinsText.text = "00:00";
        }

        private void PowerTimer()
        {
            while (MainGameStatsHolder.Timers.DamageMultiplierTimer > 0)
            {
                float damage = MainGameStatsHolder.Timers.DamageMultiplierTimer;
                if (damage < 0) damage = 0;
                powerText.text = ConvertTimeToMinutesSeconds(damage);
                MainGameStatsHolder.Timers.DamageMultiplierTimer -= Time.deltaTime;
            }
            powerText.text = "00:00";
        }

        private void ExplosionsTimer()
        {
            float explosions = MainGameStatsHolder.Timers.ExplosivesAttacksTimer;
            if (explosions < 0) explosions = 0;
            explosionsText.text = ConvertTimeToMinutesSeconds(explosions);
            MainGameStatsHolder.Timers.ExplosivesAttacksTimer -= Time.deltaTime;
            explosionsText.text = "00:00";
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