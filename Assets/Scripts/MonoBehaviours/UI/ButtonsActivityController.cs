using static MeteorVoyager.Assets.Scripts.GameStatsNameSpace.GameStats;
using UnityEngine;
using UnityEngine.UI;

namespace MeteorVoyager.Assets.Scripts.MonoBehaviours
{
    public class ButtonsActivityController : MonoBehaviour
    {
        [SerializeField] private GameObject ReloctionMenuButton;
        [SerializeField] private GameObject relocateButton;
        private void Update()
        {
            CheckRelocationMenuButtonActivity();
            CheckRelocateButtonActivity();
        }

        void CheckRelocationMenuButtonActivity()
        {
            if (ProgressionController.GameStage < 3)
            {
                ReloctionMenuButton.SetActive(false);
            }
            else
            {
                ReloctionMenuButton.SetActive(true);
            }
        }
        void CheckRelocateButtonActivity()
        {
            const int FONTSIZE = 64;
            Text text = relocateButton.transform.GetChild(0).gameObject.GetComponent<Text>();
            Button button = relocateButton.GetComponent<Button>();

            if (MainGameStatsHolder.Currency.Balance > InfiniteInteger.Million)
            {
                button.interactable = true;
                text.text = "RELOCATE";
                text.fontSize = FONTSIZE;
            }
            else
            {
                button.interactable = false;
                text.text = "UNABLE TO RELOCATE YET";
                text.fontSize = FONTSIZE / 2;
            }
        }
    }
}