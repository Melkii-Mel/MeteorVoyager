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
                if (@enum != MessageEnum.Advice)
                    return DataBankMessageScriptableObjects.First(o => o.MessageType == @enum);
                MessageScriptableObject[] advices = DataBankMessageScriptableObjects.Where(o => o.MessageType == MessageEnum.Advice).ToArray();
                return advices[Random.Range(0, advices.Length - 1)];
            }
        }
    }
}