using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SumoDemo
{
    public class DeathZone : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                GameManager.Instance.RemainPlayer--;
                other.gameObject.SetActive(false);
            }
            if (other.tag == "Sumo")
            {
                other.gameObject.SetActive(false);
                GameManager.Instance.RemainEnemy--;
            }
        }
    }
}
