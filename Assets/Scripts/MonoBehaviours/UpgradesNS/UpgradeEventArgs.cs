using System;

namespace MonoBehaviours.UpgradesNS
{
    public class UpgradeEventArgs
    {
        public UpgradeEventArgs(Enum upgradeEnum, int upgradeValue, int lastAmount, UpgradesButtonActions sender)
        {
            UpgradeEnum = upgradeEnum;
            UpgradeValue = upgradeValue;
            Sender = sender;
            LastAmount = lastAmount;
        }

        public Enum UpgradeEnum { get; }
        public int UpgradeValue { get; }
        
        /// <summary>
        /// Represents the amount of lvl ups that was made during the last upgrade
        /// </summary>
        public int LastAmount { get; }
        public UpgradesButtonActions Sender { get; }
    }
}