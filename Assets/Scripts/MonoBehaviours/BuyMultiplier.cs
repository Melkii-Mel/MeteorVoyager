using UnityEngine;
using UnityEngine.UI;

namespace MeteorVoyager.Assets.Scripts.MonoBehaviours
{
    public class BuyMultiplier : MonoBehaviour
    {
        public static int multiplier;
        void Start()
        {
            multiplier = -1;
            SwitchBuyingMultiplier();
        }

        void Update()
        {

        }

        public void SwitchBuyingMultiplier()
        {
            switch (multiplier)
            {
                case 1: multiplier = 10; break;
                case 10: multiplier = 100; break;
                case 100: multiplier = -1; break;
                case -1: multiplier = 1; break;
            }
            transform.GetChild(0).gameObject.GetComponent<Text>().text = multiplier != -1 ? $"X{multiplier}" : "MAX";
        }
    }
}