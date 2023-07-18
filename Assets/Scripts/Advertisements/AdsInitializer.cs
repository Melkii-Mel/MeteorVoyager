using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.Serialization;

namespace Advertisements
{
    public class AdsInitializer : MonoBehaviour, IUnityAdsInitializationListener
    {
        [FormerlySerializedAs("_androidGameId")] [SerializeField] private string androidGameId = "5183725";
        [FormerlySerializedAs("_iOSGameId")] [SerializeField] private string iOSGameId = "5183724";
        [FormerlySerializedAs("_testMode")] [SerializeField] private bool testMode;
        private string _gameId;

        private void Awake()
        {
            InitializeAds();
        }

        public void InitializeAds()
        {
            _gameId = Application.platform == RuntimePlatform.IPhonePlayer
                ? iOSGameId
                : androidGameId;
            Advertisement.Initialize(_gameId, testMode, this);
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