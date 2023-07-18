using System.IO;
using static UnityEngine.JsonUtility;

namespace Localization.Scripts
{
    public class Serializer
    {
        private Texts _texts = new() { buttonTexts = new(), currencyTexts = new(), stageTexts = new() };

        public void Serialize()
        {
            string json = ToJson(_texts);
            File.WriteAllText(Texts.FILE_PATH + "ru.json", json);
        }
    }
}
