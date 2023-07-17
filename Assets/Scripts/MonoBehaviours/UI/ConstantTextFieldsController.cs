using static MeteorVoyager.Assets.Scripts.GameStatsNameSpace.GameStats;
using TMPro;
using UnityEngine;

namespace MeteorVoyager.Assets.Scripts.MonoBehaviours.UI
{
    public class ConstantTextFieldsController : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI relocationButtonText;
        [SerializeField] TextMeshProUGUI hintButtonText;

        public void Start()
        {
            relocationButtonText.text = Texts.ButtonTexts.RelocationScreen;
            hintButtonText.text = Texts.ButtonTexts.HintButtonText;
        }
    }
}
