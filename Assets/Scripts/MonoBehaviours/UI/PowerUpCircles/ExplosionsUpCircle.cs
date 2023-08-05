using GameStatsNS;

namespace MonoBehaviours.UI.PowerUpCircles
{
    public class ExplosionsUpCircle : PowerUpCircle
    {
        protected override float GetTime()
        {
            return GameStats.MainGameStatsHolder.Timers.ExplosivesAttacksTimer;
        }
    }
}