using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HeckOff : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        GameObject currentselected = EventSystem.current.currentSelectedGameObject;
        if (currentselected == gameObject)
        {
            EventSystem.current.SetSelectedGameObject(GetComponentInParent<Button>().gameObject);
        } 
    }
}
