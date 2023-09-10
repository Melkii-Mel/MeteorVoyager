using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MonoBehaviours.DataBank
{
    public class UpgradeObject : MonoBehaviour
    {
        [SerializeField] private Image image;
        [SerializeField] private TextMeshProUGUI title;
        [SerializeField] private TextMeshProUGUI description;
        [SerializeField] private TextMeshProUGUI cost;
        [SerializeField] private Button enableDescriptionObjectAndBuyUpgradeButton;
        [SerializeField] private Button disableDescriptionObjectAndBuyUpgradeButton;
        [SerializeField] private GameObject descriptionObject;

        #region events

        public delegate void BuyButtonClickEventHandler();

        public event BuyButtonClickEventHandler OnBuying;
        

        #endregion

        private void OnDisable()
        {
            enableDescriptionObjectAndBuyUpgradeButton.onClick.RemoveListener(BuyButtonClick);
        }

        private void Awake()
        {
            descriptionObject.SetActive(false);
            enableDescriptionObjectAndBuyUpgradeButton.onClick.AddListener(BuyButtonClick);
        }

        private bool _descriptionActivated;

        private void BuyButtonClick()
        {
            void SetDescriptionObjectActive(bool value)
            {
                descriptionObject.SetActive(value);
                _descriptionActivated = value;
            }
            
            if (_descriptionActivated)
            {
                SetDescriptionObjectActive(false);
                OnBuying?.Invoke();
            }
            else
            {
                SetDescriptionObjectActive(true);
            }
        }

        public void SetImage(Sprite content)
        {
            image.sprite = content;
        }

        public void SetTitle(string content)
        {
            title.text = content;
        }

        public void SetDescription(string content)
        {
            description.text = content;
        }

        public void SetCost(string content)
        {
            cost.text = content;
        }
    }
}