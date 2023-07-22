using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static GameStatsNS.GameStats;


namespace MonoBehaviours
{
    public class CategoriesChanger : MonoBehaviour
    {
        [Serializable]
        private class Category
        {
            public List<GameObject> objects;

            [SerializeField] private bool enabled;
            public bool Enabled
            {
                get => enabled;
                set
                {
                    enabled = value;
                    SetActive(value);
                }
            }
            public bool unlocked = false;
            
            public void SetActive(bool val)
            {
                foreach (GameObject obj in objects)
                {
                    obj.SetActive(val);
                }
            }
        }
        [SerializeField] private Button categoryChanger;
        [SerializeField] private List<Category> categoriesObjects;
        
        private void Start()
        {
            MainGameStatsHolder.Progression.OnProgressionUpdate += CheckForEnabledCategories;
            CheckForEnabledCategories();
            categoryChanger.interactable = false;
            InitializeEach();
        }

        private void InitializeEach()
        {
            foreach (Category category in categoriesObjects)
            {
                category.SetActive(category.Enabled);
            }
        }

        private void Update()
        {
            categoryChanger.interactable = MainGameStatsHolder.Progression.GameStage >= 2;
        }

        private void CheckForEnabledCategories()
        {
            try
            {
                categoriesObjects[0].unlocked = true;
                categoriesObjects[1].unlocked = MainGameStatsHolder.Progression.GameStage >= 2;
                categoriesObjects[2].unlocked = MainGameStatsHolder.Progression.GameStage >= 4;
            }
            catch (Exception ex)
            {
                Debug.LogException(ex);
            }
        }
        public void ChangeMode()
        {
            for (int i = 0; i < categoriesObjects.Count; i++)
            {
                Category category = categoriesObjects[i];
                if (category.Enabled)
                {
                    category.Enabled = false;
                    for (int j = i + 1;; j++)
                    {
                        category = categoriesObjects[j < categoriesObjects.Count ? j : j - categoriesObjects.Count];
                        if (category.unlocked)
                        {
                            category.Enabled = true;
                            return;
                        }
                    }
                }
            }
        }
    }
}