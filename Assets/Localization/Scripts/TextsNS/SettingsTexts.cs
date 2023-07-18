using System;
using UnityEngine.Serialization;

namespace Localization.Scripts.TextsNS
{
    [Serializable]
    public class SettingsTexts
    {
        [FormerlySerializedAs("SettingsTitle")] public string settingsTitle;
        [FormerlySerializedAs("MusicVolume")] public string musicVolume;
        [FormerlySerializedAs("SoundsVolume")] public string soundsVolume;
        [FormerlySerializedAs("Trails")] public string trails;
        [FormerlySerializedAs("Particles")] public string particles;
        [FormerlySerializedAs("StarsAmount")] public string starsAmount;
        [FormerlySerializedAs("Language")] public string language;
    }
}
