using MonoBehaviours.DataBank.ScriptableObjects;
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
        [SerializeField] private TextMeshProUGUI lvl;
        [SerializeField] private Button enableDescriptionObjectAndBuyUpgradeButton;
        [SerializeField] private Button disableDescriptionObjectButton;
        [SerializeField] private GameObject descriptionObject;

        public UpgradeScriptableObject Values { get; private set; }

        #region events

        public delegate void BuyButtonClickEventHandler(UpgradeObject sender);

        public event BuyButtonClickEventHandler OnBuying;

        public delegate void DescriptionObjectEnable(object sender);

        public event DescriptionObjectEnable OnDescriptionObjectEnable;
        

        #endregion

        private void OnDisable()
        {
            enableDescriptionObjectAndBuyUpgradeButton.onClick.RemoveListener(BuyButtonClick);
            disableDescriptionObjectButton.onClick.RemoveListener(DisableDescriptionObjectActive);
            OnDescriptionObjectEnable -= ProcessDisableOtherDescriptionObjectsRequest;
        }

        private void OnEnable()
        {
            DisableDescriptionObjectActive();
            enableDescriptionObjectAndBuyUpgradeButton.onClick.AddListener(BuyButtonClick);
            disableDescriptionObjectButton.onClick.AddListener(DisableDescriptionObjectActive);
            OnDescriptionObjectEnable += ProcessDisableOtherDescriptionObjectsRequest;
        }

        private void Start()
        {
            if (Values is null)
            {
                Destroy(gameObject);
            }
        }

        private bool _descriptionActivated;
        
        private void DisableDescriptionObjectActive()
        {
            SetDescriptionObjectActive(false);
        }

        private void SetDescriptionObjectActive(bool value)
        {
            descriptionObject.SetActive(value);
            _descriptionActivated = value;
            if (value)
            {
                OnDescriptionObjectEnable?.Invoke(this);
            }
        }
        
        private void BuyButtonClick()
        {
            if (_descriptionActivated)
            {
                OnBuying?.Invoke(this);
            }
            else
            {
                SetDescriptionObjectActive(true);
            }
        }

        private void ProcessDisableOtherDescriptionObjectsRequest(object requestSender)
        {
            if (ReferenceEquals(requestSender, this)) return;
            
            DisableDescriptionObjectActive();
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
            cost.text = $"Cost: {content} data";
        }

        public void SetLvl(string content)
        {
            lvl.text = $"Lvl: {content}";
        }

        public void SetValues(UpgradeScriptableObject values)
        {
            Values = values;
        }
    }
}