using UnityEngine;
using static UnityEditor.Progress;

namespace SumoDemo
{
    [RequireComponent(typeof(CharacterMovement))]
    public class SumoCheck : MonoBehaviour
    {
        [SerializeField] LayerMask _chacterLayer, _boostLayer;
        [SerializeField] float _checkRadius;

        private CharacterMovement _sumoMovement;
        private CharacterAttack _attack;
        private Rigidbody targetBody;
        private bool canAttack;

        private float counter = 2f, checkRate = 2f;

        #region Unity Func.
        private void Start()
        {
            _sumoMovement = GetComponent<CharacterMovement>();
            _attack = GetComponent<CharacterAttack>();
        }
        private void FixedUpdate()
        {
            if (checkRate <= counter)
            {
                Detection();
                counter = 0;
            }
            else
            {
                counter += Time.fixedDeltaTime;
            }

            if (canAttack)
            {
                _attack.Attack(targetBody);
            }
        }
        #endregion

        #region Private Func.
        private void Detection()
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, _checkRadius, _chacterLayer);

            foreach (Collider item in colliders)
            {
                IInteractable x = item.GetComponent<IInteractable>();

                if (x.GetCollider() == _sumoMovement.GetCollider())
                {
                    continue;
                }
                else
                {
                    if (x.objectType==IInteractable.ObjectType.Boost)
                    {
                        Decision(x.GetRigidbody(), x.GetPosition(), x.objectType);
                        break;
                    }
                    else
                    {
                        Decision(x.GetRigidbody(), x.GetPosition(), x.objectType);
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
                        canAttack = true;
                        targetBody = rigidbody;
                        _sumoMovement.CanMove = false;
                    }
                    else
                    {
                        canAttack = false;
                        MoveTo(targetTransform);
                    }
                    break;

                case IInteractable.ObjectType.Boost:

                    //Debug.Log("boost");
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