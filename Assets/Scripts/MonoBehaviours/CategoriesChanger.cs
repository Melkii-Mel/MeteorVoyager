using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using static GameStatsNS.GameStats;


namespace MonoBehaviours
{
    public class CategoriesChanger : MonoBehaviour
    {
        [SerializeField] private GameObject categoryChanger;
        [SerializeField] private List<GameObject> categories;
        public static List<bool> CategoriesEnabled = new();

        private void Start()
        {
            UpdateEnabledCategoriesCount();
            MainGameStatsHolder.Progression.OnProgressionUpdate += CheckForEnabledCategories;
            CheckForEnabledCategories();
            categoryChanger.GetComponent<Button>().interactable = false;
        }

        private void Update()
        {
            UpdateEnabledCategoriesCount();
            categoryChanger.GetComponent<Button>().interactable = MainGameStatsHolder.Progression.GameStage >= 2;
        }

        private void CheckForEnabledCategories()
        {
            try
            {
                CategoriesEnabled[0] = true;
                CategoriesEnabled[1] = MainGameStatsHolder.Progression.GameStage >= 2;
                CategoriesEnabled[2] = MainGameStatsHolder.Progression.GameStage >= 4;
            }
            catch (Exception ex)
            {
                Debug.LogException(ex);
            }
        }
        public void ChangeMode()
        {
            UpdateEnabledCategoriesCount();
            for (int i = 0; i < categories.Count; i++)
            {
                GameObject category = categories[i];
                if (category.activeInHierarchy)
                {
                    category.SetActive(false);
                    for (int j = i + 1; j < CategoriesEnabled.Count + i; j++)
                    {
                        int index = j < CategoriesEnabled.Count ? j : j - CategoriesEnabled.Count;
                        bool categoryEnabled = CategoriesEnabled[index];
                        if (categoryEnabled)
                        {
                            categories[index].SetActive(true);
                            goto END_OF_LOOP;
                        }
                    }
                }
            } 
            END_OF_LOOP:;
        }
        private void UpdateEnabledCategoriesCount()
        {
            CategoriesEnabled.AddRange(from GameObject category in categories select false);
        }

    }
}