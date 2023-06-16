using Assets.Scripts.GameStatsNameSpace;
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
            if (Timers.Instance.CoinMultiplierTimer > 0 && !coinsRunning)
            {
                StartCoroutine(CoinTimer());
            }
            if (Timers.Instance.DamageMultiplierTimer > 0 && !powerRunning)
            {
                StartCoroutine(PowerTimer());
            }
            if (Timers.Instance.ExplosivesAttacksTimer > 0 && !explosionRunning)
            {
                StartCoroutine(ExplosionsTimer());
            }
        }

        IEnumerator CoinTimer()
        {
            coinsRunning = true;
            while (Timers.Instance.CoinMultiplierTimer > 0)
            {
                float coins = Timers.Instance.CoinMultiplierTimer;
                coinsText.text = ConvertTimeToMinutesSeconds(coins);
                Timers.Instance.CoinMultiplierTimer -= Time.deltaTime;
                yield return null;
            }
            coinsText.text = "00:00";
            coinsRunning = false;
        }

        IEnumerator PowerTimer()
        {
            powerRunning = true;
            while (Timers.Instance.DamageMultiplierTimer > 0)
            {
                float damage = Timers.Instance.DamageMultiplierTimer;
                powerText.text = ConvertTimeToMinutesSeconds(damage);
                Timers.Instance.DamageMultiplierTimer -= Time.deltaTime;
                yield return null;
            }
            powerText.text = "00:00";
            powerRunning = false;
        }

        IEnumerator ExplosionsTimer()
        {
            explosionRunning = true;
            while (Timers.Instance.ExplosivesAttacksTimer > 0)
            {
                float explosions = Timers.Instance.ExplosivesAttacksTimer;
                explosionsText.text = ConvertTimeToMinutesSeconds(explosions);
                Timers.Instance.ExplosivesAttacksTimer -= Time.deltaTime;
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