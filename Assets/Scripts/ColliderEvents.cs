using UnityEngine;
using System;

public class ColliderEvents : MonoBehaviour
{
    public delegate void TriggerDelegate( Collider other);
    public delegate void CollisionDelegate(Collision collision);

    public event TriggerDelegate OnTriggerEnterEvent;
    public event TriggerDelegate OnTriggerExitEvent;
    public event TriggerDelegate OnTriggerStayEvent;
    public event CollisionDelegate OnCollisionEnterEvent;
    public event CollisionDelegate OnCollisionExitEvent;
    public event CollisionDelegate OnCollisionStayEvent;

    private void OnTriggerEnter(Collider other)
    {
        OnTriggerEnterEvent?.Invoke(other);
    }

    private void OnTriggerExit(Collider other)
    {
        OnTriggerExitEvent?.Invoke(other);
    }

    private void OnTriggerStay(Collider other)
    {
        OnTriggerStayEvent?.Invoke(other);
    }

    private void OnCollisionEnter(Collision collision)
    {
        OnCollisionEnterEvent?.Invoke(collision);
    }
    private void OnCollisionExit(Collision collision)
    {
        OnCollisionExitEvent?.Invoke(collision);
    }
    private void OnCollisionStay(Collision collision)
    {
        OnCollisionStayEvent?.Invoke(collision);
    }
}
