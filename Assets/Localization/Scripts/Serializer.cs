using System.IO;
using static UnityEngine.JsonUtility;

namespace MeteorVoyager.Assets.Localization.Scripts.TextsNS
{
    public class Serializer
    {
        private Texts _texts = new() { ButtonTexts = new(), CurrencyTexts = new(), StageTexts = new() };

        public void Serialize()
        {
            string json = ToJson(_texts);
            File.WriteAllText(Texts.FILE_PATH + "ru.json", json);
        }
    }
}
