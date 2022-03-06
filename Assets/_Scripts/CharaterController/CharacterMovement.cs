using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SumoDemo
{
    [RequireComponent(typeof(Rigidbody))]
    public class CharacterMovement : MonoBehaviour, IInteractable
    {
        [SerializeField] float _walkSpeed = 2;

        private string _tag = "Sumo";
        private bool _canMove;
        private Collider _myCollider;
        private Rigidbody _rigidbody;
        private Vector3 _targetPoint;

        public Vector3 TargetPoint { get => _targetPoint; set => _targetPoint = value; }
        public bool CanMove { get => _canMove; set => _canMove = value; }
        public string Tag { get => _tag; set => _tag = value; }

        public IInteractable.ObjectType objectType => IInteractable.ObjectType.Character;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _myCollider = GetComponent<Collider>();
        }
        private void FixedUpdate()
        {
            if (CanMove)
            {
                _rigidbody.velocity =
                    new Vector3(
                        Direction(transform.position.x, TargetPoint.x) * _walkSpeed, _rigidbody.velocity.y,
                        Direction(transform.position.z, TargetPoint.z) * _walkSpeed);
            }
        }
        public Transform GetPosition()
        {
            return transform;
        }
        public Rigidbody GetRigidbody()
        {
            return _rigidbody;
        }

        private int Direction(float transform, float target)
        {
            var result = target - transform;

            if (result <= 0)
            {
                return -1;
            }
            else if (result >= 0)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public Collider GetCollider()
        {
            return _myCollider;
        }
    }
}