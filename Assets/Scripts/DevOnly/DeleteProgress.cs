using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using static GameStatsNS.GameStats;


namespace DevOnly
{
    public class DeleteProgress : MonoBehaviour
    {
        public async void DeleteAll()
        {
            await Deleter();
        }

        private async Task Deleter()
        {
            transform.GetChild(0).gameObject.GetComponent<Text>().text = "Please Restart";
            for (; ; )
            {
                MainGameStatsHolder.ResetAllSerialization();
                await Task.Delay(10);
            }
        }
    }
}