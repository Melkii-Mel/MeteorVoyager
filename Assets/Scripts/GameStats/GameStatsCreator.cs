
using MeteorVoyager.Assets.Scripts.Serialization;

namespace Assets.Scripts.GameStatsNameSpace
{
    public class GameStatsCreator
    {
        public T CreateGameStats<T>() where T : IGameStats, new()
        {
            T result = new();
            new DataLoaderSaver<IGameStats>().LoadGameStat(result);
            return result;
        }
    }
}