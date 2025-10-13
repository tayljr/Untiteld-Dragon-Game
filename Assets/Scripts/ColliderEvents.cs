using UnityEngine;
using System;

public class ColliderEvents : MonoBehaviour
{
    public delegate void TriggerDelegate(GameObject self, Collider other);
    public delegate void CollisionDelegate(GameObject self, Collision collision);

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
            OnTriggerEnterEvent?.Invoke(gameObject, other);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject != gameObject)
        {
            OnTriggerExitEvent?.Invoke(gameObject, other);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject != gameObject)
        {
            OnTriggerStayEvent?.Invoke(gameObject, other);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject != gameObject)
        {
            OnCollisionEnterEvent?.Invoke(gameObject, collision);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject != gameObject)
        {
            OnCollisionExitEvent?.Invoke(gameObject, collision);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject != gameObject)
        {
            OnCollisionStayEvent?.Invoke(gameObject, collision);
        }
    }
}
