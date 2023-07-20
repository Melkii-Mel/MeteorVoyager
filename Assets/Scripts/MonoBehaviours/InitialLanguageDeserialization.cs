using System;
using System.Linq;
using GameStatsNS;
using Localization.Scripts;
using UnityEngine;

namespace MonoBehaviours
{
    public class InitialLanguageDeserialization : MonoBehaviour
    {
        [Serializable] private class LanguageInfo
        {
            public Language language;
            public TextAsset file;
        }
        
        [SerializeField] private LanguageInfo[] files;

        private void Awake()
        {
            GameStats.Texts = LanguageDeserializer.Deserialize(files.First(i => i.language == GameStats.MainGameStatsHolder.Settings.Language).file);
        }
    }
}
