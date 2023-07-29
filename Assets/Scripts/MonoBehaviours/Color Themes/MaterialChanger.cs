using System;
using System.Collections.Generic;
using System.Linq;
using GameStatsNS;
using UnityEngine;

namespace MonoBehaviours.Color_Themes
{
    /// <summary>
    /// Changes material for a MeshRenderer component of the gameObject and all children gameObjects
    /// </summary>
    public class MaterialChanger : MonoBehaviour
    {
        [Serializable]
        private class MaterialThemePair
        {
            public Theme theme;
            public Material material;
        }
        
        [SerializeField] private MaterialThemePair[] themes;

        public void SetCurrentMainMaterial(GameObject obj, GameObject[] ignore)
        {
            Transform[] ignoreTransforms = ignore.Select(i => i.transform).ToArray();
            ChangeMaterial(FindMaterial(GameStats.MainGameStatsHolder.Settings.Theme), obj.transform, ignoreTransforms);
        }
        public void ChangeMaterial(Theme theme, GameObject obj, GameObject[] ignore)
        {            
            Transform[] ignoreTransforms = ignore.Select(i => i.transform).ToArray();
            ChangeMaterial(FindMaterial(theme), obj.transform, ignoreTransforms);
        }
        private Material FindMaterial(Theme theme)
        {
            foreach (MaterialThemePair materialThemePair in themes)
            {
                if (materialThemePair.theme == theme)
                {
                    return materialThemePair.material;
                }
            }

            throw new Exception("Material hasn't been found");
        }

        private void ChangeMaterial(Material material, Transform parentObject, Transform[] ignoreTransforms)
        {
            void SetMaterialIfPossible(Transform obj)
            {
                if (obj.TryGetComponent(out MeshRenderer mesh))
                {
                    mesh.material = material;
                }
            }
            SetMaterialIfPossible(parentObject);
            foreach (Transform child in parentObject.transform)
            {
                if (child == parentObject.transform)
                {
                    continue;
                }
                if (ignoreTransforms.Contains(child))
                {
                    continue;
                }
                ChangeMaterial(material, child, ignoreTransforms);
            }
        }
    }
}