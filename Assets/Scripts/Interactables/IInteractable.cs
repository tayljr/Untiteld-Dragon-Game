public interface IInteractable
{
    public delegate void BoolDelegate(bool value);
    public event BoolDelegate InteractEvent;
    
    public void StartInteract();
    public void StopInteract();
}
