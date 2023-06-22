using SerializationLibrary;

namespace MeteorVoyager.Assets.Scripts.GameStatsNameSpace
{
    public class Progression : Serializable<Progression>
    {
        public bool IsDamageUpgradeEnabled { get; set; }
        public int GameStage { get; set; }

        public void ResetDeletableValues()
        {
            IsDamageUpgradeEnabled = false;
        }
    }
}
