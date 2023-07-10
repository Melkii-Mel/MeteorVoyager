using Codice.Client.Common;
using MeteorVoyager.Assets.Scripts.MonoBehaviours;
using SerializationLibrary;
using System;
using System.Xml.Serialization;

namespace MeteorVoyager.Assets.Scripts.GameStatsNameSpace
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
