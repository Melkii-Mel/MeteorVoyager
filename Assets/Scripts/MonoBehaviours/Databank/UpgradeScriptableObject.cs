using UnityEngine;

namespace MonoBehaviours.DataBank
{
    [CreateAssetMenu(fileName = "DataBankUpgrade", menuName = "Custom", order = 0)]
    public class UpgradeScriptableObject : ScriptableObject
    {
        [SerializeField] private string upgradeNameIdentifier;
        [SerializeField] private string upgradeDescIdentifier;
        
        public string UpgradeNameIdentifier => upgradeNameIdentifier;
        public string UpgradeDescIdentifier => upgradeDescIdentifier;
    }
}