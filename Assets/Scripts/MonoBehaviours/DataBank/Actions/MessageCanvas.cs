using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MonoBehaviours.DataBank.Actions
{
    public class MessageCanvas : MonoBehaviour, IDataBankCanvas
    {
        #region Interface private fields
        
        [SerializeField] private Button closeButton;
        [SerializeField] private GameObject canvas;
        [SerializeField] private DataBankCircularTimer dataBankCircularTimer;
        
        #endregion

        #region Local private fields

        [SerializeField] private TextMeshProUGUI textObject;

        #endregion
        
        
        public Button CloseButton => closeButton;
        public GameObject Canvas => canvas;
        public DataBankCircularTimer DataBankCircularTimer => dataBankCircularTimer;

        public bool Init()
        {
            textObject.text = GetMessage();
            return true;
        }

        private string GetMessage()
        {
            throw new NotImplementedException();
        }
    }
}