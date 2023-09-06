using GameStatsNS;

namespace MonoBehaviours.UI.PowerUpCircles
{
    public class CoinUpCircle : PowerUpCircle
    {
        protected override float GetTime()
        {
            return GameStats.MainGameStatsHolder.Timers.CoinMultiplierTimer;
        }
    }
}