using System.Linq;
using MonoBehaviours.DataBank.Enums;
using MonoBehaviours.DataBank.ScriptableObjects;
using UnityEngine;

namespace MonoBehaviours.DataBank
{
    public class DataBankScriptableObjectsHolder
    {
        public static DataBankScriptableObjectsHolder Instance;

        public static void Instantiate(UpgradeScriptableObject[] dataBankUpgradeScriptableObjects, MessageScriptableObject[] dataBankMessageScriptableObjects)
        {
            Instance = new DataBankScriptableObjectsHolder(dataBankUpgradeScriptableObjects, dataBankMessageScriptableObjects);
        }

        private DataBankScriptableObjectsHolder(UpgradeScriptableObject[] dataBankUpgradeScriptableObjects,
            MessageScriptableObject[] dataBankMessageScriptableObjects)
        {
            
            DataBankUpgradeScriptableObjects = dataBankUpgradeScriptableObjects;
            DataBankMessageScriptableObjects = dataBankMessageScriptableObjects;
        }

        public UpgradeScriptableObject[] DataBankUpgradeScriptableObjects { get; }
        public MessageScriptableObject[] DataBankMessageScriptableObjects { get; }
        
        public UpgradeScriptableObject this[UpgradeEnum @enum] =>
            DataBankUpgradeScriptableObjects.First(o => o.UpgradeEnum == @enum);
        
        public MessageScriptableObject this[MessageEnum @enum] 
        {
            get
            {
                MessageScriptableObject[] enums = DataBankMessageScriptableObjects.Where(o => o.MessageType == @enum).ToArray();
                return enums[Random.Range(0, enums.Length)];
            }
        }
    }
}