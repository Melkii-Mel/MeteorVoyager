using SerializationLibrary;

namespace GameStatsNS.GameStatsTypes
{
    public class Currency : Serializable<Currency>
    {
        public InfiniteInteger Data { get; set; } = 0;
        public InfiniteInteger Balance { get; set; } = 0;
    }
}
