using MeteorVoyager.Assets.Scripts.Serialization.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MeteorVoyager.Assets.Scripts.Serialization
{
    public class AutoSaver<T> where T : IMySerializable
    {
        DataLoaderSaver<T> _dataLoaderSaver = new();
        public static List<T> GameStats { get; private set; } = new();

        async void StartAutoSave()
        {
            await AutoSave();
        }
        async Task AutoSave()
        {
            while (true)
            {
                _dataLoaderSaver.SaveAllGameStats(GameStats.ToArray());
                await Task.Delay(1000);
            }
        }
        public AutoSaver()
        {
            StartAutoSave();
        }
    }
}