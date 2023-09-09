using UnityEngine;
using UnityEngine.UI;

namespace MonoBehaviours.DataBank.Canvases
{
    public class UpgradesCanvas : MonoBehaviour, IDataBankCanvas
    {
        [SerializeField] private Button closeButton;
        [SerializeField] private GameObject canvas;
        [SerializeField] private DataBankCircularTimer dataBankCircularTimer;

        [SerializeField] private UpgradeObject[] upgradeObjects;

        private UpgradeCanvasContentManager _contentManager;

        public Button CloseButton => closeButton;

        public GameObject CanvasPrefab => canvas;
        public Transform Transform => transform;
        public GameObject GameObject => gameObject;

        public DataBankCircularTimer DataBankCircularTimer => dataBankCircularTimer;

        private Controller _dataBankController;
        public event IDataBankCanvas.ExitButtonClickEventHandler OnExit;
        public void Exit()
        {
            OnExit?.Invoke();
        }
        
        public void CheckTimerValue(float value)
        {
            if (value <= 0) Exit();
        }

        public bool Init()
        {
            _contentManager = new();
            return _contentManager.SetUpgrades(upgradeObjects);
        }
    }
}