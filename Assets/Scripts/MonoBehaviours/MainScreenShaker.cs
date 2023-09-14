using System;
using GameStatsNS;
using MonoBehaviours.UpgradesNS;
using UnityEngine;
using UnityEngine.Serialization;

namespace MonoBehaviours
{
    public class MainScreenShaker : MonoBehaviour
    {
        [SerializeField] private Camera mainCamera;
        [SerializeField] private float shakingAmplitude;
        [SerializeField] private float frequency;
        [SerializeField] private float shakingTime;

        private float ScreenShakeSettingMultiplier => GameStats.MainGameStatsHolder.Settings.ScreenShake;

        public void OnEnable()
        {
            UpgradesButtonActions.OnUpgrade += Shake;
        }

        public void OnDisable()
        {
            UpgradesButtonActions.OnUpgrade -= Shake;
        }
        private void Shake(UpgradeEventArgs args)
        {
            float multiplier = Mathf.Log(args.LastAmount, 2);
            StartCoroutine(Shaker.StartShaking(
                mainCamera.transform,
                shakingAmplitude * multiplier * ScreenShakeSettingMultiplier,
                shakingTime * multiplier,
                frequency * multiplier));
        }
    }
}