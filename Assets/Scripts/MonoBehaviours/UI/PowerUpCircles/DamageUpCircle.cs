using GameStatsNS;

namespace MonoBehaviours.UI.PowerUpCircles
{
    public class DamageUpCircle : PowerUpCircle
    {
        protected override float GetTime()
        {
            return GameStats.MainGameStatsHolder.Timers.DamageMultiplierTimer;
        }
    }
}