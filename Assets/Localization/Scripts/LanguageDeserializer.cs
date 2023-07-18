using System.IO;
using UnityEngine;

namespace Localization.Scripts
{
    public enum Languages
    {
        En,
        Ru,
    }
    public class LanguageDeserializer
    {
        public static Texts Deserialize(Languages language)
        {
            string fileName = $"{Texts.FILE_PATH}{language}.json";
            string json = File.ReadAllText(fileName);
            return JsonUtility.FromJson<Texts>(json);
        }
    }
}
