using System.Collections;
using UnityEngine;

public class HoldInteract : MonoBehaviour, IInteractable
{
    
    public event IInteractable.BoolDelegate InteractEvent;
    public bool isPressed;
    public float holdTime;

    public void StartInteract()
    {
        isPressed = true;
        StartCoroutine(Hold());
    }
    public void StopInteract()
    {
       StopCoroutine(Hold());
       isPressed = false;
       InteractEvent?.Invoke(false);
    }

    IEnumerator Hold()
    {
        yield return new WaitForSeconds(holdTime);
        if (isPressed)
        {
            InteractEvent?.Invoke(true);
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
