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

        public void Attack(Rigidbody rigidbody)
        {
            Vector3 force = _pushForce + Power;
            Vector3 resist = rigidbody.transform.localScale / 2;

            rigidbody.AddForce(force - resist, ForceMode.Force);
        }
    }
}