using UnityEngine;

namespace SumoDemo.ObjectPool
{
    public class CustomObject : MonoBehaviour, IPooledObject, IInteractable
    {
        [SerializeField] new string tag;

        #region Properties
        public string Tag { get => tag; set => tag = value; }
        public IInteractable.ObjectType objectType => IInteractable.ObjectType.Boost;
        #endregion

        #region Unity Func.
        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Sumo" || other.tag == "Player")
            {
                CharacterCollect x = other.GetComponent<CharacterCollect>();
                x.GetPower(0.1f);
                x.GetPoint(100);
                DeactivateMe();
            }
        }
        #endregion

        #region Public Func.
        public void OnObjectSpawned()
        {
            if (!gameObject.activeSelf)
            {
                gameObject.SetActive(true);
            }

            ObjectPooler.Instance.SpawnedObjects++;
        }
        public void DeactivateMe()
        {
            ObjectPooler.Instance.RelaseObject(Tag, gameObject);
            ObjectPooler.Instance.SpawnedObjects--;
        }
        public Transform GetPosition()
        {
            return transform;
        }

        public Rigidbody GetRigidbody()
        {
            return null;
        }
        public Collider GetCollider()
        {
            return null;
        }
        #endregion

        #region Private Func.

        #endregion
    }
}