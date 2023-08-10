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