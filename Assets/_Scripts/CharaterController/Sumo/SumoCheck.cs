using UnityEngine;
using static UnityEditor.Progress;

namespace SumoDemo
{
    [RequireComponent(typeof(CharacterMovement))]
    public class SumoCheck : MonoBehaviour
    {
        [SerializeField] LayerMask _chacterLayer, _boostLayer;
        [SerializeField] float _checkRadius;
        [SerializeField] float _checkRate = 0.5f;

        private CharacterMovement _sumoMovement;
        private CharacterAttack _attack;
        private Rigidbody _targetBody;
        private float _counter = 2f;
        private bool _canAttack;

        #region Unity Func.
        private void Start()
        {
            _sumoMovement = GetComponent<CharacterMovement>();
            _attack = GetComponent<CharacterAttack>();
        }
        private void FixedUpdate()
        {
            if (_checkRate <= _counter)
            {
                Detection();
                _counter = 0;
            }
            else
            {
                _counter += Time.fixedDeltaTime;
            }

            if (_canAttack)
            {
                _attack.Attack(_targetBody);
            }
        }
        #endregion

        #region Private Func.
        private void Detection()
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, _checkRadius, _chacterLayer);

            foreach (Collider item in colliders)
            {
                IInteractable interactable = item.GetComponent<IInteractable>();

                if (interactable.GetCollider() == _sumoMovement.GetCollider())
                {
                    continue;
                }
                else
                {
                    if (interactable.objectType == IInteractable.ObjectType.Boost)
                    {
                        Decision(interactable.GetRigidbody(), interactable.GetPosition(), interactable.objectType);
                        break;
                    }
                    else
                    {
                        Decision(interactable.GetRigidbody(), interactable.GetPosition(), interactable.objectType);
                    }
                }
            }
        }
        private void Decision(Rigidbody rigidbody, Transform targetTransform, IInteractable.ObjectType type)
        {
            switch (type)
            {
                case IInteractable.ObjectType.Character:

                    if (Vector3.Distance(transform.position, targetTransform.position) <= _attack.AttackRange)
                    {
                        _canAttack = true;
                        _targetBody = rigidbody;
                    }
                    else
                    {
                        _canAttack = false;
                        MoveTo(targetTransform);
                    }
                    break;

                case IInteractable.ObjectType.Boost:

                    MoveTo(targetTransform);
                    break;
            }
        }
        private void MoveTo(Transform targetTransform)
        {
            _sumoMovement.CanMove = true;
            _sumoMovement.TargetPoint = targetTransform.position;
        }
        #endregion
    }
}