using System.Collections.Generic;
using GameStatsNS;
using MonoBehaviours.DataBank.ScriptableObjects;
using UnityEngine;

namespace MonoBehaviours.DataBank
{
    public class UpgradePicker
    {
        private readonly UpgradeScriptableObject[] _objs;
        
        public UpgradePicker()
        {
            _objs = Resources.LoadAll<UpgradeScriptableObject>("ScriptableObjects");
        }
        
        public List<UpgradeScriptableObject> GetUpgrade(int amount)
        {
            List<UpgradeScriptableObject> result = new();
            List<int> ignoreIndexes = new();
            var maxIndex = _objs.Length - 1;
            while (result.Count < amount)
            {
                if (ignoreIndexes.Count == _objs.Length) break;
                
                int index = Random.Range(0, maxIndex);
                
                var obj = _objs[index];
                if (ignoreIndexes.Contains(index)) continue;
                
                if (obj.LvL > 0 && obj.OneTimeUpgrade ||
                    obj.Cost > GameStats.MainGameStatsHolder.Currency.Data)
                {
                    ignoreIndexes.Add(index);
                }
                
                result.Add(_objs[index]);
                ignoreIndexes.Add(index);
            }
            
            if (ignoreIndexes.Count < amount) result = null;
            return result;
        }
    }
}