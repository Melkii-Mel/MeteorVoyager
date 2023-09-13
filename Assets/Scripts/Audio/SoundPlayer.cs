using System;
using System.Collections;
using GameStatsNS;
using MonoBehaviours;
using MonoBehaviours.UpgradesNS;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Audio
{
    public class SoundPlayer : MonoBehaviour
    {
        [SerializeField] private GameObject emptyObject;
        [SerializeField] private ClipHolder impactClip;
        [SerializeField] private ClipHolder shotClip;
        [SerializeField] private ClipHolder chargedShotClip;
        [SerializeField] private ClipHolder destroyClip;
        [SerializeField] private ClipHolder[] upgradeClip;
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

        [Serializable]
        private class ClipHolder
        {
            [SerializeField] private AudioClip clip;
            [SerializeField] private float cooldownMS;
            private DateTime _lastGetting;
            public AudioClip Clip
            {
                get
                {
                    _lastGetting = DateTime.Now;
                    return clip;
                }
            }
            /// <summary>
            /// Cooldown sets when Clip getter is used
            /// </summary>
            public bool OnCooldown => (DateTime.Now - _lastGetting).TotalSeconds < cooldownMS / 1000;

            public ClipHolder()
            {
                _lastGetting = DateTime.Now;
            }
        }
        private class TemporaryPlayer : MonoBehaviour
        {
            private AudioSource _audioSource;

            private void Start()
            {
                _audioSource = GetComponent<AudioSource>();
            }

            private void Update()
            {
                if (!_audioSource.isPlaying) Destroy(gameObject);
            }
        }
    }
}
