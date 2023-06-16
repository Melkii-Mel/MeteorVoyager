using MeteorVoyager.Assets.Scripts.Serialization.Interfaces;

namespace Assets.Scripts.GameStatsNameSpace
{
    public interface IGameStats : IMySerializable
    {
        /// <summary>
        /// Adds an object to AutoSaver to automatically save it. Simpliest Realisation: AutoSaver.GameStats.Add(this);
        /// </summary>
        public void AddToAutoSaver();
    }
}