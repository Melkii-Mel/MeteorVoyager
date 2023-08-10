using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MonoBehaviours.DataBank.Actions
{
    public class MessageCanvas : MonoBehaviour, IDataBankCanvas
    {
        [SerializeField] private GameObject upgradeCanvas;
        [SerializeField] private Button closeButton;
        [SerializeField] private Slider slider;
        [SerializeField] private TextMeshProUGUI text;
        [SerializeField] private GameObject canvas;
        [SerializeField] private Timer timer;
        
        public Button CloseButton => closeButton;
        public GameObject Canvas => canvas;
        public Timer Timer => timer;

        public bool Init()
        {
            return true;
        }
    }
}