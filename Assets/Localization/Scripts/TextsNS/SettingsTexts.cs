using System;
using UnityEngine.Serialization;

namespace Localization.Scripts.TextsNS
{
    [Serializable]
    public class SettingsTexts
    {
        public string TargetFrameRate { get; set; } = "text";
        public string SettingsTitle { get; set; } = "text";
        public string MusicVolume { get; set; } = "text";
        public string SoundsVolume { get; set; } = "text";
        public string Trails { get; set; } = "text";
        public string Particles { get; set; } = "text";
        public string StarsAmount { get; set; } = "text";
        public string Language { get; set; } = "text";
        public string SafeScreen { get; set; } = "text";
    }
}
