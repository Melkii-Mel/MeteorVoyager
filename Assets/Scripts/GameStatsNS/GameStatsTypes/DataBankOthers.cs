using System.Collections.Generic;
using MonoBehaviours.DataBank.Enums;
using SerializationLibrary;

namespace GameStatsNS.GameStatsTypes
{
    public class DataBankOthers : Serializable<DataBankOthers>
    {
        public bool DataBankVisited = false;
        private readonly List<MessageEnum> _messagesShown = new();

        public bool IsMessageShown(MessageEnum @enum)
        {
            return _messagesShown.Contains(@enum);
        }

        /// <summary>
        /// return true if message successfully added to the list of shown messages,
        /// returns false if message was already added to the list
        /// </summary>
        public bool AddShownMessage(MessageEnum @enum)
        {
            if (_messagesShown.Contains(@enum)) return false;
            _messagesShown.Add(@enum);
            return true;
        }
    }
}