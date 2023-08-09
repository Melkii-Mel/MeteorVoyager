using System;
using UnityEditor;
using UnityEngine;

namespace MonoBehaviours.DataBank
{
    public class UpgradeCanvasContentManager : MonoBehaviour
    {
        private UpgradeObject[] _objects;
        [SerializeField] private UpgradeScriptableObject[] upgrades;
        private UpgradePicker _upgradePicker;
        
        private void Start()
        {
            _upgradePicker = new(upgrades);
            _objects = Resources.LoadAll<UpgradeObject>("ScriptableObjects");
        }

        public bool SetUpgrades()
        {
            var scriptableObjects = _upgradePicker.GetUpgrade(_objects.Length);
            if (scriptableObjects == null) return false;
            for (var i = 0; i < scriptableObjects.Count; i++)
            {
                var sObj = scriptableObjects[i];
                var obj = _objects[i];
                obj.SetTitle(sObj.UpgradeName);
                obj.SetDescription(sObj.UpgradeDesc);
                obj.SetImage(sObj.Sprite);
                obj.SetCost(sObj.Cost.ToString());
            }

            return true;
        }
    }
}