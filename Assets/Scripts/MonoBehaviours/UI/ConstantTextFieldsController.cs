using TMPro;
using UnityEngine;
using static GameStatsNS.GameStats;

namespace MonoBehaviours.UI
{
    public class ConstantTextFieldsController : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI relocationButtonText;
        [SerializeField] private TextMeshProUGUI hintButtonText;

        public void Start()
        {
            relocationButtonText.text = Texts.ButtonTexts.RelocationScreen;
            hintButtonText.text = Texts.ButtonTexts.HintButtonText;
        }
    }
}
