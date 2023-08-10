using UnityEngine;

namespace MonoBehaviours.DataBank
{
    public class UpgradeCanvasContentManager
    {
        private UpgradeScriptableObject[] _upgrades;
        private UpgradePicker _upgradePicker;
        
        private void Start()
        {
            _upgradePicker = new(_upgrades);
            _upgrades = Resources.LoadAll<UpgradeScriptableObject>("ScriptableObjects");
        }

        public bool SetUpgrades(UpgradeObject[] objects)
        {
            var scriptableObjects = _upgradePicker.GetUpgrade(objects.Length);
            if (scriptableObjects == null) return false;
            for (var i = 0; i < scriptableObjects.Count; i++)
            {
                var sObj = scriptableObjects[i];
                var obj = objects[i];
                obj.SetTitle(sObj.UpgradeName);
                obj.SetDescription(sObj.UpgradeDesc);
                obj.SetImage(sObj.Sprite);
                obj.SetCost(sObj.Cost.ToString());
            }
            return true;
        }
    }
}