using UnityEngine;
using System;

public class Interactor : MonoBehaviour
{
    public float interactDistance = 5f;
    public Vector3 interactOffset = new Vector3(0f, 1f, 0f);
    private IInteractable interactable;

    public void Interact(bool start)
    {
        if (start)
        {
            Ray ray = new Ray(transform.position + interactOffset, transform.forward);

            if (Physics.Raycast(ray, out RaycastHit hit, interactDistance, Int32.MaxValue,
                    QueryTriggerInteraction.Ignore))
            {
                Debug.Log(hit.collider.name);
                interactable = hit.collider.GetComponentInChildren<IInteractable>();

                if (interactable != null)
                {
                    interactable.StartInteract();
                }
            }
        }
        else
        {
            interactable.StopInteract();
        }
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
