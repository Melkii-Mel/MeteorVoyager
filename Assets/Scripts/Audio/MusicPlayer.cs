using System;
using GameStatsNS;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Audio
{
    public class MusicPlayer : MonoBehaviour
    {
        [SerializeField] private AudioSource source;
        [SerializeField] private AudioClip[] music;

        private float _currentDelay;
        private int _lastDelay;
        private bool _timerEnabled;
        private int DelayS => GameStats.MainGameStatsHolder.Settings.MusicDelayS;

        private static bool _instantiated = false;
        
        
        private void OnEnable()
        {
            GameStats.MainGameStatsHolder.Settings.OnDelayUpdate += DelaySettingUpdated;
            GameStats.MainGameStatsHolder.Settings.OnMusicVolumeUpdate += SetSourceVolume;
        }

        private void OnDisable()
        {
            GameStats.MainGameStatsHolder.Settings.OnDelayUpdate -= DelaySettingUpdated;
            GameStats.MainGameStatsHolder.Settings.OnMusicVolumeUpdate -= SetSourceVolume;
        }

        private void Awake()
        {
            if (_instantiated)
            {
                Destroy(gameObject);
            }
            _instantiated = true;
            DontDestroyOnLoad(this);
            _lastDelay = DelayS;
        }

        private void Update()
        {
            if (_timerEnabled)
            {
                _currentDelay -= Time.deltaTime;
            }
            if (_currentDelay < 0)
            {
                _timerEnabled = false;
                _currentDelay = DelayS;
                PlayRandom();
            }
            if (!source.isPlaying)
            {
                _timerEnabled = true;
            }
        }

        private void PlayRandom()
        {
            source.volume = GameStats.MainGameStatsHolder.Settings.MusicVolume;
            source.PlayOneShot(music[Random.Range(0, music.Length)]);
        }

        #region event functions

        //event function
        private void SetSourceVolume(float newValue)
        {
            source.volume = newValue;
        }
        
        //event function
        private void DelaySettingUpdated(int newValue)
        {
            float timeElapsed = _lastDelay - _currentDelay;
            _currentDelay = newValue - timeElapsed;
            _lastDelay = newValue;
        }

        #endregion
    }
}