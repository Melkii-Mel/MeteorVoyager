using static GameStatsNS.GameStats;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Advertisements
{
    public class RewardedAds : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
    {
        [FormerlySerializedAs("_showAdButton")] [SerializeField] private Button showAdButton;
        [FormerlySerializedAs("_androidAdUnitId")] [SerializeField] private string androidAdUnitId = "Rewarded_Android";
        [FormerlySerializedAs("_iOSAdUnitId")] [SerializeField] private string iOSAdUnitId = "Rewarded_iOS";
        private string _adUnitId; // This will remain null for unsupported platforms

        private void Awake()
        {
            // Get the Ad Unit ID for the current platform:
#if UNITY_IOS
            _adUnitId = _iOSAdUnitId;
#elif UNITY_ANDROID
            _adUnitId = androidAdUnitId;
#endif

            //Disable the button until the ad is ready to show:
            showAdButton.interactable = false;
        }

        private void Start()
        {
            LoadAd();
        }
        // Load content to the Ad Unit:
        public void LoadAd()
        {
            // IMPORTANT! Only load content AFTER initialization (in this example, initialization is handled in a different script).
            Advertisement.Load(_adUnitId, this);
        }

        // If the ad successfully loads, add a listener to the button and enable it:
        public void OnUnityAdsAdLoaded(string adUnitId)
        {
            if (adUnitId.Equals(_adUnitId))
            {
                // Configure the button to call the ShowAd() method when clicked:
                showAdButton.onClick.AddListener(ShowAd);
                // Enable the button for users to click:
                showAdButton.interactable = true;
            }
        }

        // Implement a method to execute when the user clicks the button:
        public void ShowAd()
        {
            // Disable the button:
            showAdButton.interactable = false;
            // Then show the ad:
            Advertisement.Show(_adUnitId, this);
        }

        // Implement the Show Listener's OnUnityAdsShowComplete callback method to determine if the user gets a reward:
        public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState showCompletionState)
        {
            if (adUnitId.Equals(_adUnitId) && showCompletionState.Equals(UnityAdsShowCompletionState.COMPLETED))
            {
                if (MainGameStatsHolder.Timers.X10Reward < 0)
                {
                    MainGameStatsHolder.Timers.X10Reward = 300;
                }
                else
                {
                    MainGameStatsHolder.Timers.X10Reward += 300;
                }
                //Load another ad:
                Advertisement.Load(_adUnitId, this);
            }
        }

        // Implement Load and Show Listener error callbacks:
        public void OnUnityAdsFailedToLoad(string adUnitId, UnityAdsLoadError error, string message)
        {
            // Use the error details to determine whether to try to load another ad.
        }

        public void OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message)
        {
            // Use the error details to determine whether to try to load another ad.
        }

        public void OnUnityAdsShowStart(string adUnitId) { }
        public void OnUnityAdsShowClick(string adUnitId) { }

        private void OnDestroy()
        {
            // Clean up the button listeners:
            showAdButton.onClick.RemoveAllListeners();
        }
    }
}

//if (adUnitId.Equals(adID) && showCompletionState.Equals(UnityAdsShowCompletionState.COMPLETED))
//{
//    if (GameStats.x10Reward < 0)
//    {
//        GameStats.x10Reward = 300;
//    }
//    else
//    {
//        GameStats.x10Reward += 300;
//    }
//    Debug.Log("Unity Ads Rewarded Ad Completed");
//}