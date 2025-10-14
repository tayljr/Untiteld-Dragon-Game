using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.ProBuilder;

public class HoldInteract : MonoBehaviour, IInteractable
{
    
    public event IInteractable.BoolDelegate InteractEvent;
    public bool isPressed;
    public float holdTime;
    private float pressedTime;
    
    public TMP_Text holdCounterText;
    
    public void StartInteract(GameObject interactor)
    {
        isPressed = true;
        pressedTime = 0;
        if (holdCounterText != null)
        {
            holdCounterText.text = (holdTime - pressedTime).ToString();
        }
        //StartCoroutine(Hold());
    }
    public void StopInteract(GameObject interactor)
    {
       //StopCoroutine(Hold());
       isPressed = false;
       pressedTime = 0;
       if (holdCounterText != null)
       {
           holdCounterText.text = (holdTime - pressedTime).ToString();
       }
       InteractEvent?.Invoke(false);
    }

    // IEnumerator Hold()
    // {
    //     yield return new WaitForSeconds(holdTime);
    //     if (isPressed)
    //     {
    //         InteractEvent?.Invoke(true);
    //     }
    // }
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isPressed)
        {
            pressedTime += Time.deltaTime;
            if (holdCounterText != null)
            {
                holdCounterText.text = Mathf.CeilToInt(holdTime - pressedTime).ToString();
            }
            if (pressedTime >= holdTime)
            {
                pressedTime = holdTime;
                InteractEvent?.Invoke(true);
            }
        }
    }
}
