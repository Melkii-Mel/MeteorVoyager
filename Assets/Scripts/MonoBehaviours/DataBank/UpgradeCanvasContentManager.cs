using MonoBehaviours.DataBank.ScriptableObjects;

namespace MonoBehaviours.DataBank
{
    public class UpgradeCanvasContentManager
    {
        private readonly UpgradeScriptableObject[] _upgrades;
        private readonly UpgradePicker _upgradePicker;
        
        public UpgradeCanvasContentManager()
        {
            _upgradePicker = new UpgradePicker();
        }

        public bool SetUpgrades(UpgradeObject[] uo)
        {
            var scriptableObjects = _upgradePicker.GetUpgrade(uo.Length);
            if (scriptableObjects == null) return false;
            for (var i = 0; i < scriptableObjects.Count; i++)
            {
                var sObj = scriptableObjects[i];
                var obj = uo[i];
                obj.SetTitle(sObj.UpgradeName);
                obj.SetDescription(sObj.UpgradeDesc);
                obj.SetImage(sObj.Sprite);
                obj.SetCost(sObj.Cost.ToString());
            }
            return true;
        }
    }
}