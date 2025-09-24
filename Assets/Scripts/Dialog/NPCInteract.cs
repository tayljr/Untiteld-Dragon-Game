using UnityEngine;

public class NPCInteract : MonoBehaviour, IInteractable
{
    public bool hasInteract = false;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        hasInteract = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public event IInteractable.BoolDelegate InteractEvent;

    public void StartInteract(GameObject interactor)
    {
        //interactor.GetComponent<PlayerController>().enabled = false;
        hasInteract = true;
    }

    public void StopInteract(GameObject interactor)
    {
        
    }
}
