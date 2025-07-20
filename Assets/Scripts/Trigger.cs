using System;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Trigger : MonoBehaviour
{
    public event Action OnEntered;
    public event Action OnExited;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Thief>(out _))
        {
            OnEntered?.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<Thief>(out _))
        {
            OnExited?.Invoke();
        }
    }
}