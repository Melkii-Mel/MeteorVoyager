using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MonoBehaviours.DataBank
{
    public class Behaviour : MonoBehaviour
    {
        [SerializeField] private Vector2 spawnPosition;
        [SerializeField] private Vector2 stayPosition;
        [SerializeField] private Vector2 despawnPosition;
        [SerializeField] private float fromSpawnToStayTime;
        [SerializeField] private float fromStayToDespawnTime;
        [SerializeField] private GameObject dataBank;
        [SerializeField] private UpgradesCanvas upgradesCanvas;
        private GameObject _currentDataBank;
        
        public void Spawn()
        {
            _currentDataBank = Instantiate(dataBank);
            StartCoroutine(MoveToStayPosition());
        }
        
        public void StartDespawn()
        {
            StartCoroutine(Despawn());
        }

        private IEnumerator MoveToStayPosition()
        {
            yield return StartCoroutine(Move(spawnPosition, stayPosition, fromSpawnToStayTime, _currentDataBank.transform));
            _currentDataBank.transform.position = stayPosition;
            Instantiate(upgradesCanvas).Init(this);
        }

        private IEnumerator Despawn()
        {
            yield return StartCoroutine(Move(stayPosition, despawnPosition, fromStayToDespawnTime, _currentDataBank.transform));
            Destroy(_currentDataBank);
        }

        private IEnumerator Move(Vector2 start, Vector2 end, float time, Transform objTransform)
        {
            Vector2 positionDelta = end - start;
            for (float i = 0; i < time; i += Time.deltaTime)
            {
                objTransform.position = start + positionDelta * (i / time);
                yield return null;
            }
        }
    }
}