using System;
using System.Collections.Generic;
using MonoBehaviours.DataBank.Enums;
using SerializationLibrary;

namespace GameStatsNS.GameStatsTypes
{
#pragma warning disable CS0618
    public class DataBankOthers : Serializable<DataBankOthers>
    {
        public bool DataBankVisited { get; set; } = false;
        public bool Message0Shown { get; set; } = false;
        public bool Message1Shown { get; set; } = false;
    }
#pragma warning restore CS0618
}
