using System.Collections;
using UnityEngine;

public class PressInteract : MonoBehaviour, IInteractable
{
    public event IInteractable.BoolDelegate InteractEvent;

    public void StartInteract(GameObject interactor)
    {
        InteractEvent?.Invoke(true);
    }
    public void StopInteract(GameObject interactor)
    {
        InteractEvent?.Invoke(false);
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
