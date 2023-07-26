using TMPro;
using UnityEngine;

namespace MonoBehaviours
{
    public class BuyMultiplier : MonoBehaviour
    {
        public static int Multiplier;

        private void Start()
        {
            Multiplier = -1;
            SwitchBuyingMultiplier();
        }
        public void SwitchBuyingMultiplier()
        {
            switch (Multiplier)
            {
                case 1: Multiplier = 10; break;
                case 10: Multiplier = 100; break;
                case 100: Multiplier = -1; break;
                case -1: Multiplier = 1; break;
            }
            transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = Multiplier != -1 ? $"X{Multiplier}" : "MAX";
        }
    }
}