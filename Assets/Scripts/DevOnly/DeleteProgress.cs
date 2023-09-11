using UnityEngine;
using static GameStatsNS.GameStats;


namespace DevOnly
{
    public class DeleteProgress : MonoBehaviour
    {
        public void DeleteAll()
        {
            MainGameStatsHolder.ResetAllSerialization();
            Application.Quit();
        }
    }
}