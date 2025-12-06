using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SettingToggleHandler : MonoBehaviour, ISubmitHandler
{
    [SerializeField]
    private Toggle toggle;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        toggle = GetComponentInChildren<Toggle>();
    }
    public void OnSubmit(BaseEventData eventData)
    {
        toggle.isOn = !toggle.isOn;
    }


}
