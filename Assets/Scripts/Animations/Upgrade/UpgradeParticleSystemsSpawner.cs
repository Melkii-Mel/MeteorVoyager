using System;
using GameStatsNS;
using MonoBehaviours.UpgradesNS;
using UnityEngine;

namespace Animations.Upgrade
{
    public class UpgradeParticleSystemsSpawner : MonoBehaviour
    {
        [SerializeField] private ParticleSystem system;
        
        private void OnEnable()
        {
            UpgradesButtonActions.OnUpgrade += Spawn;
        }

        private void OnDisable()
        {
            UpgradesButtonActions.OnUpgrade -= Spawn;
        }
        
        private void Spawn(UpgradeEventArgs args)
        {
            if (!GameStats.MainGameStatsHolder.Settings.ParticlesEnabled) return;
            int amount = (int)Mathf.Log(args.LastAmount + 1, 3) + 1;
            for (int i = 0; i < amount; i++)
            {
                Instantiate(system, transform).Play();
                Debug.Log("spawned");
            }
        }
    }
}
