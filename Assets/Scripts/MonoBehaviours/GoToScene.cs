using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

namespace MonoBehaviours
{
    public class GoToScene : MonoBehaviour
    {
        [FormerlySerializedAs("SceneName")] [SerializeField] private string sceneName;
        public void OnClick()
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}
