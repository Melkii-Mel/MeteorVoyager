using System.IO;
using static UnityEngine.JsonUtility;

namespace MeteorVoyager.Assets.Localization.Scripts.TextsNS
{
    public class Serializer
    {
        private const string RELATIVE_PATH = "Assets\\Localization\\LanguageFiles\\";
        private Texts _texts = new() { ButtonTexts = new(), CurrencyTexts = new(), StageTexts = new() };

        public void Serialize()
        {
            string json = ToJson(_texts);
            File.WriteAllText(RELATIVE_PATH + "ru.json", json);
        }
    }
}
