using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using UnityEngine.UI;

public class SettingSliderHandler : MonoBehaviour, IMoveHandler
{
    private AudioSource source;
    [SerializeField]
    private Slider slider;
    [SerializeField]
    private TextMeshProUGUI valueText;


    private void Start()
    {
        source = GetComponent<AudioSource>();
        slider = GetComponentInChildren<Slider>();
    }

    private void Update()
    {
            valueText.text = (slider.value).ToString("0"+ "%");

    }

    public void OnMove(AxisEventData eventData)
    {
        if (eventData.moveDir == MoveDirection.Right)
        {
            slider.value += 0.05f;
        }
        else if (eventData.moveDir == MoveDirection.Left)
        {
            slider.value -= 0.05f;

        }
    }
}
