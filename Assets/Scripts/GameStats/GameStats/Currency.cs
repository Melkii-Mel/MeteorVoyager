using MeteorVoyager.Assets.Scripts;
using SerializationLibrary;
using System;

namespace MeteorVoyager.Assets.Scripts.GameStatsNameSpace
{
    public class Currency : Serializable<Currency>
    {
        public InfiniteInteger Data { get; set; } = 0;
        public InfiniteInteger Balance { get; set; } = 0;

        public void ResetDeletableValues()
        {
            Balance = 0;
        }
    }
}
