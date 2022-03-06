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
                GameManager.Instance.TextUpdate("You Lose");
                other.gameObject.SetActive(false);
            }
            if (other.tag == "Sumo")
            {
                other.gameObject.SetActive(false);
                GameManager.Instance.RemainEnemy--;

                if (GameManager.Instance.RemainEnemy <= 0)
                {
                    GameManager.Instance.TextUpdate("You Win");
                }
            }
        }
    }
}
