using UnityEngine;
using UnityEngine.Advertisements;

namespace MeteorVoyager.Assets.Scripts.Advertisements
{
    public class AdsInitializer : MonoBehaviour, IUnityAdsInitializationListener
    {
        [SerializeField] string _androidGameId = "5183725";
        [SerializeField] string _iOSGameId = "5183724";
        [SerializeField] bool _testMode;
        private string _gameId;

        void Awake()
        {
            InitializeAds();
        }

        public void InitializeAds()
        {
            _gameId = Application.platform == RuntimePlatform.IPhonePlayer
                ? _iOSGameId
                : _androidGameId;
            Advertisement.Initialize(_gameId, _testMode, this);
        }

        public void OnInitializationComplete()
        {
            GetComponent<RewardedAds>().enabled = true;
        }

        public void OnInitializationFailed(UnityAdsInitializationError error, string message)
        {
        }
    }
}