using UnityEngine;

namespace SumoDemo
{
    [RequireComponent(typeof(Rigidbody))]
    public class SumoMovement : MonoBehaviour
    {
        [SerializeField] float _attackRate = 1.5f;
       // private Rigidbody rigidbody;
        private Transform _destination;
        private bool _canMove = false;

        #region Unity Func.
        private void Start()
        {
          //  rigidbody = GetComponent<Rigidbody>();
        }
        private void Update()
        {
            if (_canMove)
            {
                transform.position = Vector3.Lerp(transform.position, _destination.position, Time.fixedDeltaTime);
            }
        }     
        #endregion

        #region Public Func.
        public void GoTo(Transform targetPoint)
        {
            _destination = targetPoint;
            _canMove = true;
        }
        public void Stop()
        {
            _canMove = false;
        }
        #endregion
    }
}