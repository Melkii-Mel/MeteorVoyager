using System;
using System.Collections;
using Audio.Tools;
using GameStatsNS;
using MonoBehaviours;
using MonoBehaviours.UpgradesNS;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

namespace Audio
{
    public partial class SoundPlayer : MonoBehaviour
    {
        [SerializeField] private GameObject emptyObject;
        [SerializeField] private ClipHolder impactClip;
        [SerializeField] private ClipHolder shotClip;
        [SerializeField] private ClipHolder chargedShotClip;
        [SerializeField] private ClipHolder destroyClip;
        [SerializeField] private ClipHolder[] upgradeClip;
        [SerializeField] private ClipHolder[] clickClip;
        private void OnEnable()
        {
            Enemy.OnAnyEnemyDestroy += PlayDestroyClip;
            Player.OnShot += PlayShotSound;
            Enemy.OnAnyEnemyDamageTaken += PlayImpactSound;
            Player.OnChargedShot += PlayChargedShotSound;
            UpgradesButtonActions.OnUpgrade += PlayUpgradeClip;
            
        }

        private void OnDisable()
        {
            Enemy.OnAnyEnemyDestroy -= PlayDestroyClip;
            Player.OnShot -= PlayShotSound;
            Enemy.OnAnyEnemyDamageTaken -= PlayImpactSound;
            Player.OnChargedShot -= PlayChargedShotSound;
            UpgradesButtonActions.OnUpgrade -= PlayUpgradeClip;
        }
        private void PlayImpactSound(Enemy _)
        {
            PlayIfNotOnCooldown(impactClip);
        }

        private void PlayShotSound()
        {
            PlayIfNotOnCooldown(shotClip);
        }

        private void PlayChargedShotSound()
        {
            PlayIfNotOnCooldown(chargedShotClip);
        }

        private void PlayDestroyClip(Enemy _)
        {
            PlayIfNotOnCooldown(destroyClip);
        }

        private void PlayUpgradeClip(UpgradeEventArgs args)
        {
            IEnumerator UpgradePlayer(int amount)
            {
                for (int i = 0; i < amount; i++)
                {
                    PlayIfNotOnCooldown(upgradeClip[Random.Range(0, upgradeClip.Length)], pitchMultiplier: i / 5f + 1);
                    yield return null;
                }
            }

            StartCoroutine(UpgradePlayer((int)Mathf.Log(args.LastAmount, 2) + 1));
        }

        

        private void PlayIfNotOnCooldown(ClipHolder clipHolder, float pitchMultiplier = 1)
        {
            if (clipHolder.OnCooldown)
            {
                return;
            }
            
            GameObject player = Instantiate(emptyObject);
            player.AddComponent<TemporaryPlayer>();
            AudioSource audioSource = player.AddComponent<AudioSource>();
            audioSource.volume = GameStats.MainGameStatsHolder.Settings.SoundsVolume;
            audioSource.pitch = Random.Range(0.8f, 1.2f) * pitchMultiplier;
            audioSource.PlayOneShot(clipHolder.Clip);
        }
    }
}
