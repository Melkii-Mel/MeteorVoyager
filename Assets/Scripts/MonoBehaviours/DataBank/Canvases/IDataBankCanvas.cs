using UnityEngine;
using UnityEngine.UI;

namespace MonoBehaviours.DataBank.Canvases
{
    public interface IDataBankCanvas
    {
        /// <summary>
        /// Method that is triggered when the DataBank is needed to do an action
        /// </summary>
        public bool Init();
        /// <summary>
        /// Time after which the DataBank will fly away
        /// </summary>
        public DataBankCircularTimer DataBankCircularTimer { get; }
        /// <summary>
        /// Button that closes the canvas
        /// </summary>
        public Button CloseButton { get; }
        /// <summary>
        /// Basically, its the game object that will be activated, so make sure its inactive
        /// </summary>
        public GameObject CanvasPrefab { get; }

        public Transform Transform { get; }
        public GameObject GameObject { get; }
        public delegate void ExitButtonClickEventHandler();
        public event ExitButtonClickEventHandler OnExit;
    }
}