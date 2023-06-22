using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace MeteorVoyager.Assets.Scripts.MonoBehaviours
{
    public class CategoriesChanger : MonoBehaviour
    {
        [SerializeField] GameObject categoryChanger;
        [SerializeField] List<GameObject> categories;
        public static List<bool> categoriesEnabled = new();
        void Start()
        {
            UpdateEnabledCategoriesCount();
            CheckForEnabledCategories();
            categoryChanger.GetComponent<Button>().interactable = false;
        }

        void Update()
        {
            UpdateEnabledCategoriesCount();
            CheckForEnabledCategories();
            categoryChanger.GetComponent<Button>().interactable = GameProgression.gameStage >= 2;
        }

        void CheckForEnabledCategories()
        {
            try
            {
                categoriesEnabled[0] = true;
                categoriesEnabled[1] = GameProgression.gameStage >= 2;
                categoriesEnabled[2] = GameProgression.gameStage >= 4;
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
                    for (int j = i + 1; j < categoriesEnabled.Count + i; j++)
                    {
                        int index = j < categoriesEnabled.Count ? j : j - categoriesEnabled.Count;
                        bool categoryEnabled = categoriesEnabled[index];
                        if (categoryEnabled)
                        {
                            categories[index].SetActive(true);
                            goto ENDOFLOOP;
                        }
                    }
                }
            }
        ENDOFLOOP:;
        }
        public void UpdateEnabledCategoriesCount()
        {
            categoriesEnabled.AddRange(from GameObject category in categories select false);
        }

    }
}