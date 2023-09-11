using GameStatsNS;
using UnityEngine;
using UnityEngine.UI;

namespace MonoBehaviours.DataBank.Canvases
{
    public class UpgradesCanvas : MonoBehaviour, IDataBankCanvas
    {
        [SerializeField] private Button closeButton;
        [SerializeField] private GameObject canvas;
        [SerializeField] private DataBankCircularTimer dataBankCircularTimer;

        /// <summary>
        /// upgrade buttons of the canvas
        /// </summary>
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
            dataBankCircularTimer.OnTimeEnd += (_, _) => Exit();
            bool success = _contentManager.SetUpgrades(upgradeObjects);
            
            foreach (UpgradeObject upgradeObject in upgradeObjects)
            {
                try
                {
                    upgradeObject.OnBuying += Buying;
                }
                catch
                {
                    // ignored
                }
            }
            
            return success;
        }

        private InfiniteInteger Data
        {
            get => GameStats.MainGameStatsHolder.Currency.Data;
            set => GameStats.MainGameStatsHolder.Currency.Data = value;
        }

        private void Buying(UpgradeObject sender)
        {
            if (sender.Values.Cost > Data) return;
            Data -= sender.Values.Cost;
            sender.Values.LvL++;
            _contentManager.UpdateUpgrade(sender);
        }
    }
}