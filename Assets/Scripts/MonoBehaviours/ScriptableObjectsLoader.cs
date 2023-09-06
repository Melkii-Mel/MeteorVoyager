using MonoBehaviours.DataBank;
using MonoBehaviours.DataBank.ScriptableObjects;
using UnityEngine;

namespace MonoBehaviours
{
    public class ScriptableObjectsLoader : MonoBehaviour
    {
        private void Start()
        {
            MessageScriptableObject[] messageScriptableObjects =
                Resources.LoadAll<MessageScriptableObject>("ScriptableObjects");
            Debug.Log($"{messageScriptableObjects.Length} Message Scriptable Objects has been successfully loaded");
            UpgradeScriptableObject[] upgradeScriptableObjects =
                Resources.LoadAll<UpgradeScriptableObject>("ScriptableObjects");
            Debug.Log($"{upgradeScriptableObjects.Length} Upgrade Scriptable Objects has been successfully loaded");

            DataBankScriptableObjectsHolder.Instantiate(upgradeScriptableObjects, messageScriptableObjects);
        }
    }
}