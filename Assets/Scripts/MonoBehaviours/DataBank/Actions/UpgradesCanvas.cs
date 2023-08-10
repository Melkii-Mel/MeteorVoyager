using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MonoBehaviours.DataBank.Actions
{
    public class UpgradesCanvas : MonoBehaviour, IDataBankCanvas
    {
        [SerializeField] private Button closeButton;
        [SerializeField] private GameObject canvas;
        [SerializeField] private Timer timer;

        [SerializeField] private UpgradeObject[] upgradeObjects;

        private readonly UpgradeCanvasContentManager _contentManager = new();
        
        public Button CloseButton => closeButton;
        
        public GameObject Canvas => canvas;
        
        public Timer Timer => timer;
        
        private Behaviour _dataBankBehaviour;

        public bool Init()
        {
            return _contentManager.SetUpgrades(upgradeObjects);
        }
    }
}