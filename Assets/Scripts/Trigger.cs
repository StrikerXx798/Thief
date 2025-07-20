using System;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Trigger : MonoBehaviour
{
    public event Action TriggerEntered;
    public event Action TriggerExited;

    private void OnTriggerEnter(Collider _)
    {
        TriggerEntered?.Invoke();
    }

    private void OnTriggerExit(Collider _)
    {
        TriggerExited?.Invoke();
    }
}