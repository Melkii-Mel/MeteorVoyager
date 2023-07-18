using System;
using UnityEngine.Serialization;

namespace Localization.Scripts.TextsNS
{
    [Serializable]
    public class TimersTexts
    {
        [FormerlySerializedAs("CoinMultiplierTimerText")] public string coinMultiplierTimerText;
        [FormerlySerializedAs("DamageMultiplierTimerText")] public string damageMultiplierTimerText;
        [FormerlySerializedAs("ExplosionsTimerText")] public string explosionsTimerText;
    }
}
