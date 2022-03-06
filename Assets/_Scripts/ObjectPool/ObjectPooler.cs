using System.Collections.Generic;
using UnityEngine;

namespace SumoDemo.ObjectPool
{
    [RequireComponent(typeof(ObjectSpawner))]
    public class ObjectPooler : MonoBehaviour
    {
        [SerializeField] bool _isDebug;
        [SerializeField] bool _isDontDestroy;
        [SerializeField] List<Pool> _pools;

        [System.Serializable]
        public struct Pool
        {
            [SerializeField] string tag;
            [SerializeField] private GameObject objectPrefab;
            [SerializeField] int amount;

            public GameObject ObjectPrefab { get => objectPrefab; }
            public string Tag { get => tag; }
            public int Amount { get => amount; }
        }

        private static ObjectPooler _instance;
        private Dictionary<string, Queue<GameObject>> _poolDictionary;
        private uint _objectTypeCount;
        private uint _spawnedObjects;

        #region Properties
        public static ObjectPooler Instance { get => _instance; private set => _instance = value; }
        public bool IsDontDestroy { get => _isDontDestroy; }
        public uint SpawnedObjects
        {
            //Changes in CustomObject class with interface
            get => _spawnedObjects;
            set
            {
                _spawnedObjects = value;

                if (_isDebug)
                {
                    Log("SpawnedObjects_Propety", _spawnedObjects.ToString());
                }
            }
        }
        public uint ObjectTypeCount { get => _objectTypeCount; set => _objectTypeCount = value; }
        #endregion

        #region Unity Func.
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;

                if (IsDontDestroy)
                {
                    DontDestroyOnLoad(gameObject);
                }
            }
            else
            {
                Destroy(gameObject);
            }

            _poolDictionary = new Dictionary<string, Queue<GameObject>>();

            foreach (Pool item in _pools)
            {
                Queue<GameObject> objectPool = new Queue<GameObject>();

                CreateObjects(item, objectPool);
            }

            ObjectTypeCount = (uint)_poolDictionary.Count;
        }
        #endregion

        #region Public Func.
        public GameObject GetObject(string tag, Vector3 spawnPoint, Vector3 rotation = default)
        {
            if (!_poolDictionary.ContainsKey(tag))
            {
                Log(nameof(GetObject), "Böyle bir tag bulunmamaktadýr!!!!! " + tag);
                return null;
            }

            GameObject pooledObject;

            if (_poolDictionary[tag].Count == 0)
            {
                pooledObject = GetObjectFromPool(_pools[0]);
            }
            else
            {
                pooledObject = _poolDictionary[tag].Dequeue();
            }

            pooledObject.SetActive(true);
            pooledObject.transform.position = spawnPoint;
            pooledObject.transform.rotation = rotation == default ? Quaternion.identity : Quaternion.Euler(rotation);

            IPooledObject pooledObjectInterface = pooledObject.GetComponent<IPooledObject>();

            if (pooledObjectInterface != null)
            {
                pooledObjectInterface.OnObjectSpawned();
            }
            else
            {
                Log(nameof(GetObject), "The object do not have IPooledObject interface.");
            }

            Log(nameof(GetObject), "The object is returned");

            return pooledObject;
        }

        public void RelaseObject(string tag, GameObject obj)
        {
            _poolDictionary[tag].Enqueue(obj);
            obj.SetActive(false);
        }
        public void ReleaseAllObject()
        {
            CustomObject[] remainObjects = FindObjectsOfType<CustomObject>();

            for (int i = 0; i < remainObjects.Length; i++)
            {
                RelaseObject(remainObjects[i].Tag, remainObjects[i].gameObject);
            }

            SpawnedObjects = 0;
        }
        #endregion

        #region Private Func.
        private void CreateObjects(Pool pool, Queue<GameObject> objectPool)
        {
            for (int i = 0; i < pool.Amount; i++)
            {
                objectPool.Enqueue(GetObjectFromPool(pool));
            }

            _poolDictionary.Add(pool.Tag, objectPool);
        }
        private GameObject GetObjectFromPool(Pool pool)
        {
            GameObject obj = Instantiate(pool.ObjectPrefab);
            obj.SetActive(false);
            obj.transform.parent = transform;

            return obj;
        }
        private void Log(string methodName, string message)
        {
            if (_isDebug)
            {
                Debug.Log(methodName + " - " + message + " " + gameObject.name);
            }
        }
        #endregion
    }

    internal interface IPooledObject
    {
        void OnObjectSpawned();
        void DeactivateMe();
    }
}