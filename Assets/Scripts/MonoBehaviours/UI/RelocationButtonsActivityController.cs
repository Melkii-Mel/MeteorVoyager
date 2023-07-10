using MeteorVoyager.Assets.Scripts.GameStatsNameSpace;
using UnityEngine;
using UnityEngine.UI;
using static MeteorVoyager.Assets.Scripts.GameStatsNameSpace.GameStats;

namespace MeteorVoyager.Assets.Scripts.MonoBehaviours
{
    public class RelocationButtonsActivityController : MonoBehaviour
    {
        [SerializeField] private GameObject ReloctionMenuButton;
        [SerializeField] private GameObject relocateButton;

        private void Start()
        {
            MainGameStatsHolder.Progression.OnProgressionUpdate += CheckRelocationMenuButtonActivity;
        }

        private void Update()
        {
            CheckRelocateButtonActivity();
        }

        void CheckRelocationMenuButtonActivity()
        {
            if (MainGameStatsHolder.Progression.GameStage < 3)
            {
                ReloctionMenuButton.SetActive(false);
            }
            else
            {
                ReloctionMenuButton.SetActive(true);
                MainGameStatsHolder.Progression.OnProgressionUpdate -= CheckRelocateButtonActivity;
            }
        }
        void CheckRelocateButtonActivity()
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