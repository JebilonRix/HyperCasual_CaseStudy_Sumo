using UnityEngine;

namespace SumoDemo
{
    [RequireComponent(typeof(CharacterMovement))]
    public class CharacterInput : MonoBehaviour
    {
        [SerializeField] Camera _camera;
        private CharacterMovement _movement;
        private CharacterAttack _attack;
        private RaycastHit _hit;

        private void Start()
        {
            _movement = GetComponent<CharacterMovement>();
            _attack = GetComponent<CharacterAttack>();
        }
        private void Update()
        {
            if (Input.GetMouseButton(0))
            {
                Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out _hit))
                {
                    if (_hit.collider.CompareTag("Ground"))
                    {
                        SetMovement(true, new Vector3(_hit.point.x, 0, _hit.point.z));
                    }
                    if (_hit.collider.CompareTag("Player"))
                    {
                        return;
                    }
                    if (_hit.collider.CompareTag("Sumo"))
                    {
                        if (Vector3.Distance(transform.position, _hit.transform.position) <= _attack.AttackRange)
                        {
                            _attack.Attack(_hit.rigidbody);
                            Debug.DrawRay(transform.position, _hit.transform.position);
                        }
                        else
                        {
                            SetMovement(true, new Vector3(_hit.point.x, 0, _hit.point.z));
                        }
                    }
                }
            }
            if (Input.GetMouseButtonUp(0))
            {
                SetMovement(false, Vector3.zero);
            }
        }
        private void SetMovement(bool canMove, Vector3 point)
        {
            _movement.CanMove = canMove;
            _movement.TargetPoint = point;
        }
    }
}