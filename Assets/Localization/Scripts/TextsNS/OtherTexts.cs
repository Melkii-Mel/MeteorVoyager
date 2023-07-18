using System;
using UnityEngine.Serialization;

namespace Localization.Scripts.TextsNS
{
    [Serializable]
    public class OtherTexts
    {
        [FormerlySerializedAs("DataUponRelocation")] public string dataUponRelocation = "При скачке ты получишь {0} единиц данных";
    }
}
