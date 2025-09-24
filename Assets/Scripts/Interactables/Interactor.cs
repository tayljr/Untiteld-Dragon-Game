using UnityEngine;
using System;

public class Interactor : MonoBehaviour
{
    public float interactDistance = 5f;
    public Vector3 interactOffset = new Vector3(0f, 1f, 0f);
    private IInteractable interactable;
    private Vector3 interactPosition;
    public void Interact(bool start)
    {
        if (start)
        {
            Ray ray = new Ray(transform.position + interactOffset, transform.forward);

            if (Physics.Raycast(ray, out RaycastHit hit, interactDistance, Int32.MaxValue,
                    QueryTriggerInteraction.Ignore))
            {
                //Debug.Log(hit.collider.name);
                interactable = hit.collider.GetComponentInChildren<IInteractable>();
                interactPosition = hit.point;
                if (interactable != null)
                {
                    interactable.StartInteract(gameObject);
                }
            }
        }
        else
        {
            if (interactable != null)
            {
                interactable.StopInteract(gameObject);
            }
        }
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, interactPosition) > interactDistance)
        {
            if (interactable != null)
            {
                interactable.StopInteract(gameObject);
            }
        }
    }
}
