using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SumoDemo
{
    [RequireComponent(typeof(CharacterAttack))]
    public class CharacterCollect : MonoBehaviour
    {
        [SerializeField] Vector3 _expansion = new Vector3(0.05f, 0.05f, 0.05f);

        private CharacterAttack _attack;
        private float _powerUpgrade;
        private uint _point;

        public uint Money { get => _point; set => _point = value; }

        private void Start()
        {
            _attack = GetComponent<CharacterAttack>();
        }
        public void GetPoint(uint moneyAmount)
        {
            Money += moneyAmount;
        }
        public void GetPower(float amount)
        {
            _powerUpgrade += amount;
            _attack.Power = new Vector3(_powerUpgrade, 0, _powerUpgrade);
            transform.localScale += _expansion;
        }
    }
}
