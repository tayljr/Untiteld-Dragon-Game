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
        if (other.gameObject != gameObject)
        {
            OnTriggerEnterEvent?.Invoke(other);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject != gameObject)
        {
            OnTriggerExitEvent?.Invoke(other);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject != gameObject)
        {
            OnTriggerStayEvent?.Invoke(other);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject != gameObject)
        {
            OnCollisionEnterEvent?.Invoke(collision);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject != gameObject)
        {
            OnCollisionExitEvent?.Invoke(collision);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject != gameObject)
        {
            OnCollisionStayEvent?.Invoke(collision);
        }
    }
}
