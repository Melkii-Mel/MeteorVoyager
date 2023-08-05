using System;
using System.Globalization;
using TMPro;
using UnityEngine;
using static GameStatsNS.GameStats;

namespace MonoBehaviours
{
    public class PowerUpController : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI coinsText;
        [SerializeField] private TextMeshProUGUI powerText;
        [SerializeField] private TextMeshProUGUI explosionsText;

        private void OnEnable()
        {
            GlobalTimer.OnTick += HandleTimers;
            MainGameStatsHolder.Progression.OnProgressionUpdate += SetActivity;
        }

        private void OnDisable()
        {
            GlobalTimer.OnTick -= HandleTimers;
            MainGameStatsHolder.Progression.OnProgressionUpdate -= SetActivity;

        }

        private void Awake()
        {
            OnEnable();
            SetActivity();
        }

        private int GetGameStage()
        {
            return MainGameStatsHolder.Progression.GameStage;
        }

        private void SetActivity()
        {
            if (GetGameStage() <= 2)
            {
                Switch(false);
            }
            Switch(true);
            MainGameStatsHolder.Progression.OnProgressionUpdate -= SetActivity;
        }

        private void Switch(bool state)
        {
            coinsText.gameObject.SetActive(state);
            powerText.gameObject.SetActive(state);
            explosionsText.gameObject.SetActive(state);
        }

        private void Update()
        {
            HandleTimers(0);
        }

        private void HandleTimers(float deltaTimeMS)
        {
            CoinTimer();
            PowerTimer();
            ExplosionsTimer();
        }

        private void CoinTimer()
        {
            if (MainGameStatsHolder.Timers.CoinMultiplierTimer > 0)
            {
                float coins = MainGameStatsHolder.Timers.CoinMultiplierTimer;
                coinsText.text = ConvertSToHundredthOfS(coins);
                return;
            }
            coinsText.text = "0";
        }

        private void PowerTimer()
        {
            if (MainGameStatsHolder.Timers.DamageMultiplierTimer > 0)
            {
                float damage = MainGameStatsHolder.Timers.DamageMultiplierTimer;
                powerText.text = ConvertSToHundredthOfS(damage);
                return;
            }
            powerText.text = "0";
        }

        private void ExplosionsTimer()
        {
            if (MainGameStatsHolder.Timers.ExplosivesAttacksTimer > 0)
            {
                float explosions = MainGameStatsHolder.Timers.ExplosivesAttacksTimer;
                explosionsText.text = ConvertSToHundredthOfS(explosions);
                return;
            }
            explosionsText.text = "0";
        }

        // private string ConvertTimeToMinutesSeconds(float input)
        // {
        //     int minutes = (int)(input - 0.01f) / 60;
        //     float seconds = input - 0.01f - minutes * 60;
        //     string result = $"{minutes}:{(int)seconds}";
        //     return result;
        // }
        private string ConvertSToHundredthOfS(float inputS)
        {
            return $"{(int)(inputS * 100)}";
        }
    }
}