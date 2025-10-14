using System.Collections;
using UnityEngine;

public class ToggleInteract : MonoBehaviour, IInteractable
{
    public event IInteractable.BoolDelegate InteractEvent;
    
    public bool isToggled;

    public void StartInteract(GameObject interactor)
    {
        isToggled = !isToggled;
        InteractEvent?.Invoke(isToggled);
    }
    public void StopInteract(GameObject interactor)
    {
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
