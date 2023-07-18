using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameStatsNS.GameStats;

namespace MonoBehaviours
{
    public class MusicController : MonoBehaviour
    {
        [SerializeField] private List<AudioClip> musicIntros;
        [SerializeField] private List<AudioClip> music;
        [SerializeField] private AudioSource source;
        [SerializeField] private float playSpeed;
        private int _musicIndex;

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

        private IEnumerator PlayIntroAndSendCommand()
        {
            int index = CalculateIndexFromEnemiesHealth(musicIntros);
            AudioClip clip = musicIntros[index];
            float time = clip.length;
            PlaySong(clip, oneShot: true);
            yield return new WaitUntil(() => !source.isPlaying);
            StartCoroutine(nameof(MusicChanger));
            yield return new WaitForEndOfFrame();
        }

        private IEnumerator MusicChanger()
        {
            _musicIndex = CalculateIndexFromEnemiesHealth(music);
            int thisFrameMusicIndex = _musicIndex;
            PlaySong(music[thisFrameMusicIndex]);
            for (; ; )
            {
                thisFrameMusicIndex = CalculateIndexFromEnemiesHealth(music);
                if (thisFrameMusicIndex != _musicIndex)
                {
                    _musicIndex = thisFrameMusicIndex;
                    if (_musicIndex > music.Count - 1)
                    {
                        _musicIndex = music.Count - 1;
                    }
                    PlaySong(music[_musicIndex]);
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
            int index = MainGameStatsHolder.Progression.GameStage;
            if (list.Count < index + 1)
            {
                index = list.Count - 1;
            }
            return index;
        }
    }
}