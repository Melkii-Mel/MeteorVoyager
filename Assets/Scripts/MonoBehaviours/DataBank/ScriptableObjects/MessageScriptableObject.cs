using System;
using GameStatsNS;
using LanguageTextSets_for_Unity;
using MonoBehaviours.DataBank.Enums;
using UnityEngine;

namespace MonoBehaviours.DataBank.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Message", menuName = "Custom/DataBankMessage", order = 1)]
    public class MessageScriptableObject : ScriptableObject
    {
        [SerializeField] private MessageEnum messageTypeEnum;
        [SerializeField] private LanguageTextSet content;
        public MessageEnum MessageType => messageTypeEnum;
        public string Content => content
        [
            (Language)Enum.Parse(typeof(Language),
            GameStats.MainGameStatsHolder.Settings.Language.ToString())
        ];
    }
}