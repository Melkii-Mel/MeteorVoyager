using UnityEngine;
using UnityEngine.SceneManagement;

namespace MeteorVoyager.Assets.Scripts.Settings
{
    public class GoToScene : MonoBehaviour
    {
        [SerializeField] string SceneName;
        public void OnClick()
        {
            SceneManager.LoadScene(SceneName);
        }
    }
}
