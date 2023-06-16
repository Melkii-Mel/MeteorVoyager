using Assets.Scripts.GameStatsNameSpace;

namespace MeteorVoyager.Assets.Scripts.Serialization.Interfaces
{
    public interface IDeletableOnRelocation : IGameStats
    {
        void ResetDeletableValues();
    }
}
