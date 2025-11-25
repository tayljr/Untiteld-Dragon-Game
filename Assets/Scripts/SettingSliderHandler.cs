using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.EventSystems;

public class SettingSliderHandler : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    private AudioSource source;
    private void Start()
    {
        source = GetComponent<AudioSource>();
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        source.enabled = true;
        source.Play();
        source.loop = true;
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        source.enabled = false;
        source.Stop();
        source.loop = false;
    }
}
