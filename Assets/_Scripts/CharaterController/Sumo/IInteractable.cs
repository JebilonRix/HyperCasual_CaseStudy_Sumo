using UnityEngine;

namespace SumoDemo
{
    public interface IInteractable
    {
        enum ObjectType
        {
            Character = 0, Boost = 1
        }
        ObjectType objectType { get; }
        Transform GetPosition();
        Rigidbody GetRigidbody();
        Collider GetCollider();
    }
}