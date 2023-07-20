using System.IO;
using System.Xml.Serialization;
using UnityEngine;

namespace Localization.Scripts
{
    public class Serializer : MonoBehaviour
    {
        private readonly Texts _texts = new()
        {
            ButtonTexts = new(),
            TimersTexts = new(),
            CurrencyTexts = new(),
            OtherTexts = new(),
            SettingsTexts = new(),
            StageTexts = new(),
        };

        public void Serialize()
        {
            using FileStream stream = new("Assets\\Localization\\LanguageFiles\\blank.xml", FileMode.Create);
            new XmlSerializer(typeof(Texts)).Serialize(stream, _texts);
        }
    }
}
