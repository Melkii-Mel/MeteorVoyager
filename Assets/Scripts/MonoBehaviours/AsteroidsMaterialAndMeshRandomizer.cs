using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace MonoBehaviours
{
    public class AsteroidsMaterialAndMeshRandomizer : MonoBehaviour
    {
        [SerializeField] private float changingDelay;
        [SerializeField][Range(0f, 1f)] private float changingChance;
        [SerializeField][Range(0f, 1f)] private float purity;
        [SerializeField] private List<Material> materials;
        [SerializeField] private List<Mesh> meshes;


        private Material _currentMaterial;
        private Mesh _currentMesh;
        
        private float _currentTime;
        
        
        public void SetMeshAndMaterial(MeshFilter meshComponent, MeshRenderer materialComponent)
        {
            SetMaterial(materialComponent);
            SetMesh(meshComponent);
        }

        private void Start()
        {
            (_currentMaterial, _currentMesh) = Randomize();
        }
        
        private void Update()
        {
            if (_currentTime >= changingDelay)
            {
                _currentTime -= changingDelay;
                if (RandomBool(changingChance))
                {
                    (_currentMaterial, _currentMesh) = Randomize();
                }
            }
            
            _currentTime += Time.deltaTime;
        }


        private void SetMaterial(MeshRenderer meshRenderer)
        {
            if (Pure())
            {
                meshRenderer.material = _currentMaterial;
                return;
            }
            
            meshRenderer.material = Randomize().Item1;
        }

        private void SetMesh(MeshFilter meshFilter)
        {
            if (Pure())
            {
                meshFilter.mesh = _currentMesh;
                return;
            }
            meshFilter.mesh = Randomize().Item2;
        }

        private bool Pure()
        {
            return RandomBool(purity);
        }
        
        private bool RandomBool(float probability)
        {
            return (Random.Range(0f, 1f) < probability);
        }

        private (Material, Mesh) Randomize()
        {
            Material material = materials[Random.Range(0, materials.Count)];
            Mesh mesh = meshes[Random.Range(0, meshes.Count)];
            return (material, mesh);
        }
    }
}
