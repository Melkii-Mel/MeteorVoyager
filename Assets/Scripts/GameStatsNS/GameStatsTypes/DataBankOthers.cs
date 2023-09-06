using System.Collections.Generic;
using MonoBehaviours.DataBank.Enums;
using SerializationLibrary;

namespace GameStatsNS.GameStatsTypes
{
    public class DataBankOthers : Serializable<DataBankOthers>
    {
        public bool DataBankVisited = false;
        public List<MessageEnum> MessagesShown;
    }
}