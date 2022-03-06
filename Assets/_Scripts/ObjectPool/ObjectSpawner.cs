using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SumoDemo.ObjectPool
{
    [RequireComponent(typeof(ObjectPooler))]
    public class ObjectSpawner : MonoBehaviour
    {
        [SerializeField] new string tag;
        [SerializeField] Transform[] _spawnPoints;
        [SerializeField] float _spawnRate, _spawnAmount;
        [SerializeField] SpawnType _spawnType;
        [SerializeField] Dimension _dimension;

        private float xMin;
        private float xMax;
        private float yMin;
        private float yMax;
        private float zMin;
        private float zMax;

        private enum Dimension
        {
            TwoDimension, ThreeDimension
        }
        private enum SpawnType
        {
            SingleSpawnPoint, RandomSpawnPoints, RandomSpawnInArea
        }

        #region Unity Func.
        private void Awake()
        {
            if (ObjectPooler.Instance.IsDontDestroy)
            {
                DontDestroyOnLoad(gameObject);
            }
        }
        private void Start()
        {
            if (_spawnType == SpawnType.RandomSpawnInArea)
            {
                List<float> listX = new List<float>();
                List<float> listY = new List<float>();
                List<float> listZ = new List<float>();

                for (int i = 0; i < _spawnPoints.Length; i++)
                {
                    listX.Add(_spawnPoints[i].position.x);
                    listY.Add(_spawnPoints[i].position.y);
                    listZ.Add(_spawnPoints[i].position.z);
                }

                listX.Sort();
                listY.Sort();
                listZ.Sort();

                xMin = MinMaxSetter(true, listX);
                xMax = MinMaxSetter(false, listX);

                yMin = MinMaxSetter(true, listY);
                yMax = MinMaxSetter(false, listY);

                zMin = MinMaxSetter(true, listZ);
                zMax = MinMaxSetter(false, listZ);
            }

            InvokeRepeating(nameof(SpawnObject), 1, _spawnRate);
        }
        #endregion

        #region Public Func.
        public void SpawnObject()
        {
            switch (_spawnType)
            {
                case SpawnType.SingleSpawnPoint:

                    for (int i = 0; i < _spawnPoints.Length; i++)
                    {
                        ObjectPooler.Instance.GetObject(tag, _spawnPoints[i].position);
                    }
                    break;
                case SpawnType.RandomSpawnInArea:

                    ObjectPooler.Instance.GetObject(tag, SpawnRandomlyInArea());
                    break;
                case SpawnType.RandomSpawnPoints:

                    ObjectPooler.Instance.GetObject(tag, SpawnRandomlyInPoints(_spawnPoints));
                    break;
            }
        }
        public void ResetAllPools()
        {
            ObjectPooler.Instance.ReleaseAllObject();
        }
        #endregion

        #region Private Func.
        private Vector3 SpawnRandomlyInArea()
        {
            Vector3 returnVector;

            switch (_dimension)
            {
                case Dimension.TwoDimension:

                    returnVector = new Vector2
                    (
                    Random.Range(xMin, xMax),
                    Random.Range(yMin, yMax)
                    );
                    break;
                case Dimension.ThreeDimension:

                    returnVector = new Vector3
                    (
                    Random.Range(xMin, xMax),
                    Random.Range(yMin, yMax),
                    Random.Range(zMin, zMax)
                    );
                    break;
                default:

                    returnVector = Vector3.zero;
                    break;
            }

            return returnVector;
        }
        private Vector3 SpawnRandomlyInPoints(params Transform[] spawnPoints)
        {
            Vector3 returnVector;

            //Random point selected
            Transform randomPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

            switch (_dimension)
            {
                case Dimension.TwoDimension:

                    returnVector = new Vector2(randomPoint.position.x, randomPoint.position.y);
                    break;
                case Dimension.ThreeDimension:

                    returnVector = new Vector3(randomPoint.position.x, randomPoint.position.y, randomPoint.position.z);
                    break;
                default:

                    returnVector = Vector3.zero;
                    break;
            }

            return returnVector;
        }
        private float MinMaxSetter(bool isMin, List<float> list)
        {
            if (isMin)
            {
                return list[0];
            }
            else
            {
                return list[_spawnPoints.Length - 1];
            }
        }
        #endregion
    }
}