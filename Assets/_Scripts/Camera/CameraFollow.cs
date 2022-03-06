using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SumoDemo
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] GameObject target;
        [SerializeField] Vector3 followDistance;

        private void Update()
        {
            transform.position = target.transform.position + followDistance;
        }

    }
}