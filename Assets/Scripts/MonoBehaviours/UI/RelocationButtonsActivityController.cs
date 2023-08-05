using UnityEngine;
using UnityEngine.UI;
using static GameStatsNS.GameStats;

namespace MonoBehaviours.UI
{
    public class RelocationButtonsActivityController : MonoBehaviour
    {
        [SerializeField] private GameObject relocationMenuButton;
        [SerializeField] private GameObject relocateButton;

        private void OnEnable()
        {
            MainGameStatsHolder.Progression.OnProgressionUpdate += CheckRelocationMenuButtonActivity;
        }

        private void OnDisable()
        {
            MainGameStatsHolder.Progression.OnProgressionUpdate -= CheckRelocationMenuButtonActivity;
        }

        private void Awake()
        {
            CheckRelocationMenuButtonActivity();
        }

        private void Update()
        {
            CheckRelocateButtonActivity();
        }

        private void CheckRelocationMenuButtonActivity()
        {
            if (MainGameStatsHolder.Progression.GameStage < 3)
            {
                relocationMenuButton.SetActive(false);
            }
            else
            {
                relocationMenuButton.SetActive(true);
                MainGameStatsHolder.Progression.OnProgressionUpdate -= CheckRelocateButtonActivity;
            }
        }

        private void CheckRelocateButtonActivity()
        {
            Text text = relocateButton.transform.GetChild(0).gameObject.GetComponent<Text>();
            Button button = relocateButton.GetComponent<Button>();

            if (MainGameStatsHolder.Currency.Balance > InfiniteInteger.Million)
            {
                button.interactable = true; 
                text.text = $"{Texts.ButtonTexts.ConfirmRelocation}";
            }
            else
            {
                button.interactable = false;
                text.text = $"{Texts.ButtonTexts.UnableToRelocate}";
            }
        }
    }
}