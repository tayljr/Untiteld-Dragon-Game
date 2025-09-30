using UnityEngine;

public interface IInteractable
{
    public delegate void BoolDelegate(bool value);
    public event BoolDelegate InteractEvent;
    
    public void StartInteract(GameObject interactor);
    public void StopInteract(GameObject interactor);
}
