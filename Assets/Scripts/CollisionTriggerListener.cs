using System;
using UnityEngine;

public class CollisionTriggerListener : MonoBehaviour
{
    public event Action<Collider2D> OnTriggerEnter; 
    public event Action<Collider2D> OnTriggerExit; 
    public event Action<Collider2D> OnTriggerStay; 

    private void OnTriggerEnter2D(Collider2D other)
    {
        OnTriggerEnter?.Invoke(other);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        OnTriggerExit?.Invoke(other);
        Debug.Log($"Trigger exited");
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        OnTriggerStay?.Invoke(other);
    }
}