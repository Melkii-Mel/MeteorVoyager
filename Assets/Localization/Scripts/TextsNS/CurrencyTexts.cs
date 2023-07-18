using System;
using UnityEngine.Serialization;

namespace Localization.Scripts.TextsNS
{
    [Serializable]
    public class CurrencyTexts
    {
        [FormerlySerializedAs("Matter")] public string matter = "Материя";
        [FormerlySerializedAs("Data")] public string data = "Данные";
    }
}
