using MeteorVoyager.Assets.Scripts.Serialization.Interfaces;

namespace MeteorVoyager.Assets.Scripts.Serialization
{
    public class DataLoaderSaver<T> where T : IMySerializable
    {
        public void SaveAllGameStats(params T[] Serializables)
        {
            foreach (var serializable in Serializables)
            {
                Serializer.Serialize(serializable);
            }
        }

        public void SaveGameStat(T gameStats)
        {
            Serializer.Serialize(gameStats);
        }

        public void LoadAllGameStats(params T[] GameStats)
        {
            foreach (var gameStats in GameStats)
            {
                Serializer.Deserialize(gameStats);
            }
        }

        public void LoadGameStat(T gameStats)
        {
            Serializer.Deserialize(gameStats);
        }

        public void DeleteDataOnRelocation(params T[] GameStats)
        {
            foreach (T gameStats in GameStats)
            {
                if (gameStats is not IDeletableOnRelocation)
                {
                    continue;
                }
                var item = (IDeletableOnRelocation)gameStats;
                item.ResetDeletableValues();
                Serializer.Serialize(item);
            }
        }
    }
}
