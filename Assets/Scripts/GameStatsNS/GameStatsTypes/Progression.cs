using MonoBehaviours;
using SerializationLibrary;

namespace GameStatsNS.GameStatsTypes
{
    public class Progression : Serializable<Progression>
    { 
        public bool IsDamageUpgradeEnabled { get; set; }
        public int GameStage
        {
            get => _progressionController.GameStage;
            set => _progressionController.GameStage = value;
        }

        private readonly ProgressionController _progressionController;

        public delegate void ProgressionUpdateHandler();

        public event ProgressionUpdateHandler OnProgressionUpdate; 

        public Progression()
        {
            _progressionController = new(() => OnProgressionUpdate?.Invoke());
        }
    }
}
