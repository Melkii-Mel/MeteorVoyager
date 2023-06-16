using Assets.Scripts.GameStatsNameSpace;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MeteorVoyager.Assets.Scripts.MonoBehaviours
{
    public class MusicController : MonoBehaviour
    {
        [SerializeField] private List<AudioClip> musicIntros;
        [SerializeField] private List<AudioClip> music;
        [SerializeField] private AudioSource source;
        [SerializeField] private float playSpeed;
        private int musicIndex;

        private void Start()
        {
            foreach (AudioClip clip in music)
            {
                source.clip = clip;
                source.Play();
                source.Stop();
            }
            StartCoroutine(nameof(PlayIntroAndSendCommand));
        }

        private void Update()
        {
            source.pitch = playSpeed;
        }
        IEnumerator PlayIntroAndSendCommand()
        {
            int index = CalculateIndexFromEnemiesHealth(musicIntros);
            AudioClip clip = musicIntros[index];
            float time = clip.length;
            PlaySong(clip, oneShot: true);
            yield return new WaitUntil(() => !source.isPlaying);
            StartCoroutine(nameof(MusicChanger));
            yield return new WaitForEndOfFrame();
        }
        IEnumerator MusicChanger()
        {
            musicIndex = CalculateIndexFromEnemiesHealth(music);
            int thisFrameMusicIndex = musicIndex;
            PlaySong(music[thisFrameMusicIndex]);
            for (; ; )
            {
                thisFrameMusicIndex = CalculateIndexFromEnemiesHealth(music);
                if (thisFrameMusicIndex != musicIndex)
                {
                    musicIndex = thisFrameMusicIndex;
                    if (musicIndex > music.Count - 1)
                    {
                        musicIndex = music.Count - 1;
                    }
                    PlaySong(music[musicIndex]);
                }
                yield return new WaitForSeconds(1);
            }
        }
        public void PlaySong(AudioClip clip, bool oneShot = false)
        {
            if (source.isPlaying)
            {
                var time = source.time;
                source.Stop();
                source.time = time;
            }
            source.clip = clip;
            source.pitch = 1;
            source.loop = !oneShot;
            source.Play();
        }
        private static int CalculateIndexFromEnemiesHealth(List<AudioClip> list)
        {
            int index = Progression.Instance.GameStage;
            if (list.Count < index + 1)
            {
                index = list.Count - 1;
            }
            return index;
        }
    }
}