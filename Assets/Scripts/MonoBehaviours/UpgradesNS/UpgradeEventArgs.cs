using System;

namespace MonoBehaviours.UpgradesNS
{
    public class UpgradeEventArgs
    {
        public UpgradeEventArgs(Enum upgradeEnum, int upgradeValue, UpgradesButtonActions sender)
        {
            UpgradeEnum = upgradeEnum;
            UpgradeValue = upgradeValue;
            Sender = sender;
        }

        public Enum UpgradeEnum { get; }
        public int UpgradeValue { get; }
        public UpgradesButtonActions Sender { get; }
    }
}