using System;
using System.Xml.Serialization;
using MonoBehaviours;
using SerializationLibrary;

namespace GameStatsNS.GameStatsTypes
{
    public class Progression : Serializable<Progression>
    { 
        public bool IsDamageUpgradeEnabled { get; set; }
        public int GameStage
        {
            get
            {
                return _progressionController.GameStage;
            }
            set
            {
                _progressionController.GameStage = value;
            }
        }

        private ProgressionController _progressionController;
        public void ResetDeletableValues()
        {
            IsDamageUpgradeEnabled = false;
        }
        [XmlIgnore]
        public Action OnProgressionUpdate { get; set; } = delegate { };

        public Progression()
        {
            _progressionController = new(OnProgressionUpdate);
        }
    }
}
