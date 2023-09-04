using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MonoBehaviours.DataBank.Actions
{
    public interface IDataBankCanvas
    {
        /// <summary>
        /// Method that triggers when DataBank is needed to do an action
        /// </summary>
        public bool Init();
        /// <summary>
        /// Time after which the DataBank will fly away
        /// </summary>
        public DataBankCircularTimer DataBankCircularTimer { get; }
        /// <summary>
        /// Slider that will decrease its value as time passes
        /// </summary>
        public Button CloseButton { get; }
        /// <summary>
        /// Basically, its the game object that will be activated, so make sure its inactive
        /// </summary>
        public GameObject Canvas { get; }
    }
}