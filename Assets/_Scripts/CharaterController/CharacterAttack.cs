using UnityEngine;

namespace SumoDemo
{
    public class CharacterAttack : MonoBehaviour
    {
        [SerializeField] Vector3 _pushForce = new Vector3(1f, 1f, 1f);
        [SerializeField] float _attackRange = 1.5f;

        private Vector3 _power;

        public float AttackRange { get => _attackRange; set => _attackRange = value; }
        public Vector3 Power { get => _power; set => _power = value; }

        Vector3 gizmo;

        public void Attack(Rigidbody rigidbody)
        {
            // Debug.Log("Attack");
            rigidbody.velocity = new Vector3(0, rigidbody.velocity.y, 0);

            Vector3 force = _pushForce + Power;
            Vector3 resist = rigidbody.transform.localScale / 3;

            rigidbody.AddForce(force - resist, ForceMode.Impulse);
            gizmo = force;
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawRay(this.transform.position, gizmo);
        }
    }
}
