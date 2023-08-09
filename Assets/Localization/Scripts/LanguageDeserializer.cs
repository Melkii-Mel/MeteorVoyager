using System.IO;
using System.Xml.Serialization;
using UnityEngine;

namespace Localization.Scripts
{
    public static class LanguageDeserializer
    {
        public static Texts Deserialize(TextAsset languageFile)
        {
            return Deserialize(languageFile.text);
        }

        public static Texts Deserialize(string languageContent)
        {
            using TextReader stream = new StringReader(languageContent);
            return (Texts)(new XmlSerializer(typeof(Texts)).Deserialize(stream));
        }
    }
}
