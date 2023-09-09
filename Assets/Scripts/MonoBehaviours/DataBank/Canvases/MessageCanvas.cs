using GameStatsNS;
using GameStatsNS.GameStatsTypes;
using MonoBehaviours.DataBank.Enums;
using MonoBehaviours.DataBank.ScriptableObjects;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MonoBehaviours.DataBank.Canvases
{
    public class MessageCanvas : MonoBehaviour, IDataBankCanvas
    {
        #region Interface private fields
        
        [SerializeField] private Button closeButton;
        [SerializeField] private GameObject canvas;
        [SerializeField] private DataBankCircularTimer dataBankCircularTimer;
        
        #endregion

        #region Other private fields

        [SerializeField] private TextMeshProUGUI textObject;

        #endregion
        
        
        public Button CloseButton => closeButton;
        public GameObject CanvasPrefab => canvas;
        public Transform Transform => transform;
        public GameObject GameObject => gameObject;
        public event IDataBankCanvas.ExitButtonClickEventHandler OnExit;
        public DataBankCircularTimer DataBankCircularTimer => dataBankCircularTimer;

        public bool Init()
        {
            textObject.text = GetMessage();
            return true;
        }

        public void Exit()
        {
            OnExit?.Invoke();
        }

        public void CheckTimerValue(float value)
        {
            if (value <= 0) Exit();
        }

        private MessageScriptableObject FindMessageObject(MessageEnum messageEnum)
        {
            return DataBankScriptableObjectsHolder.Instance[messageEnum];
        }

        private MessageScriptableObject FindRandomAdviceObject()
        {
            return DataBankScriptableObjectsHolder.Instance[MessageEnum.Advice];
        }
        private string GetMessage()
        {
            DataBankOthers dbo = GameStats.MainGameStatsHolder.DataBankOthers;

            if (!dbo.Message0Shown)
            {
                dbo.Message0Shown = true;
                return FindMessageObject(MessageEnum.HelloDoYouKnowWhoIAm).Content;
            }

            if (!dbo.Message1Shown)
            {
                dbo.Message1Shown = true;
                return FindMessageObject(MessageEnum.DoYouKnowWhatDataIs).Content;
            }

            return FindMessageObject(MessageEnum.Advice).Content;
        }
    }
}