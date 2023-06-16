using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace MeteorVoyager.Assets.Scripts.DevOnly
{
    public class DeleteProgress : MonoBehaviour
    {
        public async void DeleteAll()
        {
            await Deleter();
        }

        async Task Deleter()
        {
            transform.GetChild(0).gameObject.GetComponent<Text>().text = "Please Restart";
            for (; ; )
            {
                PlayerPrefs.DeleteAll();
                await Task.Delay(10);
            }
        }
    }
}