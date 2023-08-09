using System.Collections;
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
        private GameObject _currentDataBank;

        #region Events

        public delegate void DataBankBehaviourEventsHandler(Behaviour behaviour, DataBankBehaviourEventArgs args);

        public event DataBankBehaviourEventsHandler OnReachingStayPosition;

        public event DataBankBehaviourEventsHandler OnSpawn;

        public event DataBankBehaviourEventsHandler OnDespawn;

        public event DataBankBehaviourEventsHandler OnLeavingStayPosition;

        #endregion
        
        public void Spawn()
        {
            OnSpawn?.Invoke(this, GenerateEventArgs());
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
            OnReachingStayPosition?.Invoke(this, GenerateEventArgs());
        }

        private IEnumerator Despawn()
        {
            OnLeavingStayPosition?.Invoke(this, GenerateEventArgs());
            yield return StartCoroutine(Move(stayPosition, despawnPosition, fromStayToDespawnTime, _currentDataBank.transform));
            OnDespawn?.Invoke(this, GenerateEventArgs());
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

        private DataBankBehaviourEventArgs GenerateEventArgs()
        {
            return new DataBankBehaviourEventArgs(dataBank);
        }
        
        public class DataBankBehaviourEventArgs
        {
            public GameObject DataBank { get; }

            public DataBankBehaviourEventArgs(GameObject dataBank)
            {
                DataBank = dataBank;
            }
        }
    }
}